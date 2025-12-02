# Optotune Pattern Workflow - Programmer Instructions

This document describes how to programmatically replicate the Control GUI's Optotune pattern workflow for running custom mirror patterns on the ODEM device. The instructions are language-agnostic with protocol details and pseudocode.

## Overview

The workflow consists of four main steps:
1. **Load X and Y Input Files** - Select or generate waveform CSV files
2. **Set Pattern Parameters** - Configure mirror frequency and FPGA points
3. **Upload Pattern to Device** - Transfer files via SCP with pattern naming convention
4. **Run Optotune with Pattern** - Execute Command 4, Sub-command 9 via TCP

---

## Step 1: Load Generated Input Files for X and Y

### File Format
Waveform files are CSV format with one floating-point value per line:
```csv
0.123456
0.234567
0.345678
...
```

### File Requirements
- Both X and Y files must have **equal line counts**
- Each line contains a single floating-point value (6 decimal places)
- Files represent mirror position commands over one complete scan cycle

### Pseudocode
```
FUNCTION load_input_files(x_file_path, y_file_path):
    # Validate files exist
    IF NOT file_exists(x_file_path):
        ERROR "X input file not found: " + x_file_path
    IF NOT file_exists(y_file_path):
        ERROR "Y input file not found: " + y_file_path
    
    # Count non-empty lines (represents Optotune/mirror points per axis)
    x_line_count = count_non_empty_lines(x_file_path)
    y_line_count = count_non_empty_lines(y_file_path)
    
    # Validate equal line counts
    IF x_line_count != y_line_count:
        ERROR "Line count mismatch: X=" + x_line_count + ", Y=" + y_line_count
    
    RETURN {
        x_file: x_file_path,
        y_file: y_file_path,
        mirror_points_per_axis: x_line_count
    }
```

---

## Step 2: Set Mirror Frequency and FPGA Points

### Key Parameters
| Parameter | Description | Typical Values |
|-----------|-------------|----------------|
| `mirror_frequency_hz` | Optotune mirror scan frequency | 0.1 - 40000.0 Hz |
| `achieved_total_fpga_points_to_use` | Total FPGA sample points for timing | Calculated from analysis |
| `mirror_points_per_axis` | Points per mirror axis (from file line count) | Same as file line count |

### Parameter JSON Format
The GUI generates a `scan_parameters.json` file with pattern metadata:

```json
{
    "metadata": {
        "timestamp": "20250126_143022",
        "generation_method": "manual",
        "generation_date": "20250126",
        "generation_time": "143022",
        "waveform_files": {
            "x_file": "waveformX_20250126_143022.csv",
            "y_file": "waveformY_20250126_143022.csv"
        }
    },
    "selected_parameters": {
        "mirror_frequency_hz": 5000.0,
        "achieved_total_fpga_points_to_use": 262143,
        "mirror_points_per_axis": 1000
    }
}
```

### Pseudocode
```
FUNCTION create_pattern_parameters(x_file, y_file, mirror_frequency_hz, fpga_points):
    # Count lines from input file for mirror points
    mirror_points = count_non_empty_lines(x_file)
    
    # Generate timestamp in YYYYMMDD_HHMMSS format
    timestamp = format_current_time("YYYYMMDD_HHMMSS")
    
    params = {
        metadata: {
            timestamp: timestamp,
            generation_method: "manual",
            waveform_files: {
                x_file: get_filename(x_file),
                y_file: get_filename(y_file)
            }
        },
        selected_parameters: {
            mirror_frequency_hz: to_float(mirror_frequency_hz),
            achieved_total_fpga_points_to_use: to_integer(fpga_points),
            mirror_points_per_axis: mirror_points
        }
    }
    
    # Save to JSON file
    params_file = "scan_parameters_" + timestamp + ".json"
    write_json_file(params_file, params)
    
    RETURN params_file, params
```

---

## Step 3: Upload Pattern Files to Device

### Device Configuration
```
DEVICE_IP     = "192.168.2.24"
PATTERN_DIR   = "/var/lib/odem/patterns"  # ODEM_PATTERN_DIR
SSH_USER      = "root"
```

### Pattern File Naming Convention
Files are uploaded using `{pattern_id}_waveformX.csv` and `{pattern_id}_waveformY.csv`:
- Pattern ID can be numeric (1-10) or string ("test1", "circle", etc.)
- The Control GUI uses a single naming convention: `{pattern_id}_waveformX.csv` and `{pattern_id}_waveformY.csv`

### Shell Commands
```bash
# Configuration
DEVICE_IP="192.168.2.24"
PATTERN_DIR="/var/lib/odem/patterns"
SSH_OPTS="-o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null"

# Ensure pattern directory exists
ssh $SSH_OPTS root@$DEVICE_IP "mkdir -p $PATTERN_DIR"

# Upload X waveform file
scp $SSH_OPTS "$X_FILE" "root@$DEVICE_IP:$PATTERN_DIR/${PATTERN_ID}_waveformX.csv"

# Upload Y waveform file
scp $SSH_OPTS "$Y_FILE" "root@$DEVICE_IP:$PATTERN_DIR/${PATTERN_ID}_waveformY.csv"

# Optionally upload parameters JSON
scp $SSH_OPTS "$PARAMS_FILE" "root@$DEVICE_IP:$PATTERN_DIR/${PATTERN_ID}_scan_parameters.json"
```

### Pseudocode
```
FUNCTION upload_pattern_files(x_file, y_file, pattern_id, params_file, device_ip):
    pattern_dir = "/var/lib/odem/patterns"
    ssh_options = "-o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null"
    
    # Ensure pattern directory exists
    result = ssh_execute(device_ip, "root", "mkdir -p " + pattern_dir, ssh_options)
    IF result.failed:
        ERROR "Failed to create directory: " + result.error
    
    # Upload X waveform file
    dest_x = "root@" + device_ip + ":" + pattern_dir + "/" + pattern_id + "_waveformX.csv"
    result_x = scp_upload(x_file, dest_x, ssh_options)
    
    # Upload Y waveform file  
    dest_y = "root@" + device_ip + ":" + pattern_dir + "/" + pattern_id + "_waveformY.csv"
    result_y = scp_upload(y_file, dest_y, ssh_options)
    
    # Optionally upload parameters JSON
    IF params_file EXISTS:
        dest_params = "root@" + device_ip + ":" + pattern_dir + "/" + pattern_id + "_scan_parameters.json"
        scp_upload(params_file, dest_params, ssh_options)
    
    # Check results
    IF result_x.failed:
        ERROR "X file upload failed: " + result_x.error
    IF result_y.failed:
        ERROR "Y file upload failed: " + result_y.error
    
    RETURN success
```

---

## Step 4: Run Optotune with Pattern

### TCP Protocol Details
- **Command:** 4 (CMD_OPTOTUNE)
- **Sub-command:** 9 (Optotune Run with Pattern)
- **Port:** 24871
- **Timeout:** 120+ seconds (operation can take 100+ seconds)

### Request Packet Format
```
Offset  Size    Field               Description
------  ----    -----               -----------
0       1       Command             0x04 (CMD_OPTOTUNE)
1       1       Sub-command         0x09 (Run with Pattern)
2-5     4       Address             0x00000000 (reserved)
6-9     4       Payload length      Big-endian, length of pattern_id + null terminator
10+     N       Pattern ID          Null-terminated ASCII string
```

### Response Header Format (8 bytes for Success, 12 bytes for Error)
```
Success Response:
Offset  Size    Field               Description
------  ----    -----               -----------
0       1       Response Type       0x00 (Success)
1       1       Command ID          Original command (0x04)
2-3     2       Reserved            0x0000
4-7     4       Payload Length      Big-endian, length of response data

Error Response:
Offset  Size    Field               Description
------  ----    -----               -----------
0       1       Response Type       0x01 (Error)
1       1       Command ID          Original command (0x04)
2-3     2       Reserved            0x0000
4-7     4       Error Msg Length    Big-endian
8-11    4       Error Code          Big-endian (see table below)
12+     N       Error Message       ASCII string

Progress Response:
Offset  Size    Field               Description
------  ----    -----               -----------
0       1       Response Type       0x02 (Progress)
1       1       Command ID          Original command (0x04)
2-3     2       Reserved            0x0000
4-7     4       Payload Length      Big-endian
8-11    4       Current Step        Big-endian (1-based)
12-15   4       Total Steps         Big-endian
16-19   4       Status Length       Big-endian
20+     N       Status Message      ASCII string
```

### Response Types
| Type | Value | Description |
|------|-------|-------------|
| Success | 0x00 | Operation completed successfully |
| Error | 0x01 | Operation failed (see error codes) |
| Progress | 0x02 | Progress update during operation |

### Error Codes
| Code | Value | Description |
|------|-------|-------------|
| PATTERN_FILES_NOT_FOUND | 0x00000008 | Pattern files not found |
| OPTOTUNE_HARDWARE_FAILURE | 0x00000009 | Hardware operation failed |
| OPTOTUNE_LOGGING_FAILURE | 0x0000000A | Logging operation failed |
| GENERAL_ERROR | 0x00000001 | General/directory creation error |

### Pseudocode
```
FUNCTION run_optotune_pattern(pattern_id, device_ip, port, timeout):
    CMD_OPTOTUNE = 0x04
    SUB_CMD_RUN_WITH_PATTERN = 0x09
    
    # Create TCP socket connection
    socket = tcp_connect(device_ip, port, timeout)
    
    TRY:
        # Build command packet
        pattern_id_bytes = ascii_encode(pattern_id) + NULL_BYTE
        payload_length = length(pattern_id_bytes)
        
        # Pack header: command(1) + sub_cmd(1) + address(4) + length(4)
        # All multi-byte fields are big-endian
        header = pack_bytes(
            CMD_OPTOTUNE,           # 1 byte
            SUB_CMD_RUN_WITH_PATTERN,  # 1 byte
            0x00000000,             # 4 bytes, address (reserved)
            payload_length          # 4 bytes, big-endian
        )
        packet = header + pattern_id_bytes
        
        # Send command
        socket_send(socket, packet)
        
        # Initialize result structure
        results = {
            success: false,
            progress_messages: [],
            output_directory: null,
            error_code: null,
            error_message: null
        }
        
        # Handle responses in a loop
        WHILE true:
            response = socket_receive(socket, 4096)
            IF response IS EMPTY:
                BREAK
            
            response_type = response[0]
            
            IF response_type == 0x02:  # Progress response
                # Parse 20-byte header + status message
                payload_len = unpack_uint32_big_endian(response[4:8])
                current_step = unpack_uint32_big_endian(response[8:12])
                total_steps = unpack_uint32_big_endian(response[12:16])
                status_len = unpack_uint32_big_endian(response[16:20])
                status_msg = ascii_decode(response[20 : 20 + status_len])
                
                results.progress_messages.append({
                    step: current_step,
                    total: total_steps,
                    percent: (current_step / total_steps) * 100,
                    message: status_msg
                })
                
                # Extract file paths from progress messages
                IF "Directory: " IN status_msg:
                    results.output_directory = split_after(status_msg, "Directory: ")
                ELSE IF "Results: " IN status_msg:
                    results.results_directory = split_after(status_msg, "Results: ")
                
                log("Progress: " + current_step + "/" + total_steps + " - " + status_msg)
                
            ELSE IF response_type == 0x00:  # Success response
                results.success = true
                log("Optotune pattern operation completed successfully")
                BREAK
                
            ELSE IF response_type == 0x01:  # Error response
                error_msg_len = unpack_uint32_big_endian(response[4:8])
                error_code = unpack_uint32_big_endian(response[8:12])
                error_msg = ascii_decode(response[12 : 12 + error_msg_len])
                
                results.error_code = error_code
                results.error_message = error_msg
                
                # Handle specific error codes
                IF error_code == 0x00000008:
                    log("Pattern files not found for '" + pattern_id + "'")
                ELSE IF error_code == 0x00000009:
                    log("Optotune hardware failure: " + error_msg)
                ELSE IF error_code == 0x0000000A:
                    log("Optotune logging failure: " + error_msg)
                ELSE:
                    log("Error " + hex(error_code) + ": " + error_msg)
                BREAK
        
        RETURN results
        
    FINALLY:
        socket_close(socket)
```

### Example: Building the Request Packet

For pattern ID "test1":
```
Byte offset:  0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15
Hex value:    04   09   00   00   00   00   00   00   00   06   74   65   73   74   31   00
              |    |    |----address----|   |--length=6--|   t    e    s    t    1   NUL
              CMD  SUB
```

---

## Complete Workflow Example (Shell Script)

```bash
#!/bin/bash
# Complete Optotune pattern workflow example

# Configuration
DEVICE_IP="192.168.2.24"
TCP_PORT=24871
PATTERN_DIR="/var/lib/odem/patterns"
SSH_OPTS="-o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null"

# Input parameters
X_FILE="$1"
Y_FILE="$2"
PATTERN_ID="$3"
MIRROR_FREQ="$4"
FPGA_POINTS="$5"

echo "=== Step 1: Validating input files ==="
if [ ! -f "$X_FILE" ]; then
    echo "ERROR: X file not found: $X_FILE"
    exit 1
fi
if [ ! -f "$Y_FILE" ]; then
    echo "ERROR: Y file not found: $Y_FILE"
    exit 1
fi

X_LINES=$(grep -c . "$X_FILE")
Y_LINES=$(grep -c . "$Y_FILE")
if [ "$X_LINES" != "$Y_LINES" ]; then
    echo "ERROR: Line count mismatch: X=$X_LINES, Y=$Y_LINES"
    exit 1
fi
echo "  X file: $X_FILE ($X_LINES lines)"
echo "  Y file: $Y_FILE ($Y_LINES lines)"

echo ""
echo "=== Step 2: Creating parameters JSON ==="
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
PARAMS_FILE="/tmp/scan_parameters_${TIMESTAMP}.json"
cat > "$PARAMS_FILE" << EOF
{
    "metadata": {
        "timestamp": "$TIMESTAMP",
        "generation_method": "manual"
    },
    "selected_parameters": {
        "mirror_frequency_hz": $MIRROR_FREQ,
        "achieved_total_fpga_points_to_use": $FPGA_POINTS,
        "mirror_points_per_axis": $X_LINES
    }
}
EOF
echo "  Created: $PARAMS_FILE"

echo ""
echo "=== Step 3: Uploading pattern files ==="
ssh $SSH_OPTS root@$DEVICE_IP "mkdir -p $PATTERN_DIR"
scp $SSH_OPTS "$X_FILE" "root@$DEVICE_IP:$PATTERN_DIR/${PATTERN_ID}_waveformX.csv"
scp $SSH_OPTS "$Y_FILE" "root@$DEVICE_IP:$PATTERN_DIR/${PATTERN_ID}_waveformY.csv"
scp $SSH_OPTS "$PARAMS_FILE" "root@$DEVICE_IP:$PATTERN_DIR/${PATTERN_ID}_scan_parameters.json"
echo "  Uploaded pattern files to device"

echo ""
echo "=== Step 4: Sending TCP command ==="
echo "  (TCP command requires language-specific socket implementation)"
echo "  See pseudocode in documentation for packet format"
echo ""
echo "  Quick test with netcat (sends packet, minimal response handling):"
echo "  printf '\\x04\\x09\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\x06${PATTERN_ID}\\x00' | nc -w 130 $DEVICE_IP $TCP_PORT"
```

### Usage
```bash
./optotune_workflow.sh waveformX.csv waveformY.csv test1 5000.0 262143
```

---

## Complete Workflow Pseudocode

```
FUNCTION complete_optotune_workflow(x_file, y_file, pattern_id, 
                                    mirror_frequency_hz, fpga_points):
    """
    Complete workflow to run Optotune with custom pattern.
    
    Parameters:
        x_file: Path to X waveform CSV file
        y_file: Path to Y waveform CSV file
        pattern_id: Pattern identifier (string or number)
        mirror_frequency_hz: Mirror scan frequency in Hz
        fpga_points: Total FPGA points for timing synchronization
    
    Returns:
        Result containing success status and output paths
    """
    
    # Configuration
    DEVICE_IP = "192.168.2.24"
    TCP_PORT = 24871
    PATTERN_DIR = "/var/lib/odem/patterns"
    
    log("=== Step 1: Loading input files ===")
    
    # Validate input files
    IF NOT file_exists(x_file):
        ERROR "X file not found: " + x_file
    IF NOT file_exists(y_file):
        ERROR "Y file not found: " + y_file
    
    # Count lines for mirror points
    x_lines = count_non_empty_lines(x_file)
    y_lines = count_non_empty_lines(y_file)
    
    IF x_lines != y_lines:
        ERROR "Line count mismatch: X=" + x_lines + ", Y=" + y_lines
    
    mirror_points = x_lines
    log("  X file: " + x_file + " (" + x_lines + " lines)")
    log("  Y file: " + y_file + " (" + y_lines + " lines)")
    log("  Mirror points per axis: " + mirror_points)
    
    log("=== Step 2: Creating pattern parameters ===")
    
    timestamp = format_current_time("YYYYMMDD_HHMMSS")
    params = {
        metadata: {
            timestamp: timestamp,
            generation_method: "manual",
            waveform_files: {
                x_file: get_filename(x_file),
                y_file: get_filename(y_file)
            }
        },
        selected_parameters: {
            mirror_frequency_hz: to_float(mirror_frequency_hz),
            achieved_total_fpga_points_to_use: to_integer(fpga_points),
            mirror_points_per_axis: mirror_points
        }
    }
    
    params_file = "/tmp/scan_parameters_" + timestamp + ".json"
    write_json_file(params_file, params)
    
    log("  Mirror frequency: " + mirror_frequency_hz + " Hz")
    log("  FPGA points: " + fpga_points)
    log("  Parameters file: " + params_file)
    
    log("=== Step 3: Uploading pattern files ===")
    
    ssh_options = "-o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null"
    
    # Create directory on device
    ssh_execute(DEVICE_IP, "root", "mkdir -p " + PATTERN_DIR, ssh_options)
    
    # Upload files
    files_to_upload = [
        {local: x_file, remote: pattern_id + "_waveformX.csv"},
        {local: y_file, remote: pattern_id + "_waveformY.csv"},
        {local: params_file, remote: pattern_id + "_scan_parameters.json"}
    ]
    
    FOR each file IN files_to_upload:
        dest = "root@" + DEVICE_IP + ":" + PATTERN_DIR + "/" + file.remote
        result = scp_upload(file.local, dest, ssh_options)
        IF result.failed:
            ERROR "Upload failed for " + file.remote + ": " + result.error
        log("  Uploaded: " + file.remote)
    
    log("=== Step 4: Running Optotune with pattern ===")
    
    # Connect via TCP and send command
    socket = tcp_connect(DEVICE_IP, TCP_PORT, timeout=130)
    
    TRY:
        # Build command packet (see Step 4 for packet format details)
        CMD_OPTOTUNE = 0x04
        SUB_CMD_RUN_WITH_PATTERN = 0x09
        pattern_bytes = ascii_encode(to_string(pattern_id)) + NULL_BYTE
        
        header = pack_bytes(
            CMD_OPTOTUNE,
            SUB_CMD_RUN_WITH_PATTERN,
            0x00000000,  # address (reserved)
            length(pattern_bytes)  # payload length, big-endian
        )
        socket_send(socket, header + pattern_bytes)
        
        # Process responses
        result = {success: false, results_directory: null}
        
        WHILE true:
            response = socket_receive(socket, 4096)
            IF response IS EMPTY:
                BREAK
            
            response_type = response[0]
            
            IF response_type == 0x02:  # Progress
                status_len = unpack_uint32_big_endian(response[16:20])
                status_msg = ascii_decode(response[20 : 20 + status_len])
                current = unpack_uint32_big_endian(response[8:12])
                total = unpack_uint32_big_endian(response[12:16])
                log("  Progress: " + current + "/" + total + " - " + status_msg)
                
                IF "Results: " IN status_msg:
                    result.results_directory = split_after(status_msg, "Results: ")
                    
            ELSE IF response_type == 0x00:  # Success
                result.success = true
                log("=== Operation completed successfully ===")
                BREAK
                
            ELSE IF response_type == 0x01:  # Error
                error_code = unpack_uint32_big_endian(response[8:12])
                error_len = unpack_uint32_big_endian(response[4:8])
                error_msg = ascii_decode(response[12 : 12 + error_len])
                result.error_code = error_code
                result.error_message = error_msg
                log("=== Operation failed: " + error_msg + " ===")
                BREAK
        
        RETURN result
        
    FINALLY:
        socket_close(socket)
```

---

## Important Notes

### Connection Timeout
- The Optotune Run with Pattern operation can take 100+ seconds
- Set socket timeout to at least 130 seconds
- Consider implementing ping suspension if using keepalive monitoring

### Pre-requisites Before Running
1. Pattern files must be uploaded to device before running
2. VPU and Logger frequencies may need to be set (Command 4, Sub-commands 12/13)
3. SCAN register values may need configuration if not using defaults


### Related Protocol Commands
| Command | Sub-cmd | Description |
|---------|---------|-------------|
| 4 | 3 | Stop Optotune operation |
| 4 | 4 | Acquire Log (download results) |
| 4 | 11 | Update LUT from Log |
| 4 | 12 | Set VPU and Logger frequencies |
| 4 | 13 | Get current frequencies |

---

## See Also

- [PROTOCOL.md](../../odem_linux_app-odem-beta/PROTOCOL.md) - Full protocol specification
- [OPTOTUNE_CONTROL.md](../../odem_linux_app-odem-beta/docs/OPTOTUNE_CONTROL.md) - Optotune control documentation
- [tabOptotuneControl.py](../tabOptotuneControl.py) - GUI implementation reference
- [tabOptotuneAnalysis.py](../tabOptotuneAnalysis.py) - Pattern generation reference
