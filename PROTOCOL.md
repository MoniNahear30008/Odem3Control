# ODEM Linux App Protocol Details

## Overview

This protocol provides TCP-based communication for controlling the ODEM (Optical Distance Measurement) embedded Linux application. The application runs on ARM Cortex-A9 with Intel Arria 10 FPGA hardware and provides real-time control of LIDAR data streaming, AWG DAC operations, Optotune mirror control, and I2C device management.

**Protocol Architecture**: The protocol is organized with logical command separation:
- **Linux endian functions** for cross-platform compatibility
- **Enum-based command system** for maintainability and type safety
- **Progress reporting system** for long-running operations with real-time status updates
- **Dedicated error codes** for specific failure conditions (e.g., pattern files not found)
- **MSGDMA commands** under dedicated Command 5 for data streaming control
- **Optotune commands** under Command 4 with comprehensive sub-command structure

## General Response Format

All responses from the server, whether indicating success or an error, begin with a unified header structure designed for 32-bit alignment.

**1. Success Response Structure (Total Header: 8 bytes)**

| Offset | Size (bytes) | Field                 | Description                                                                 |
|--------|--------------|-----------------------|-----------------------------------------------------------------------------|
| 0      | 1            | Response Type         | `0x00` (Indicates Success)                                                  |
| 1      | 1            | Original Command ID   | The command ID from the original request.                                   |
| 2-3    | 2            | Reserved              | `0x00, 0x00` (For alignment and future use)                               |
| 4-7    | 4            | Payload Length        | Big-endian. Length of the `Actual Data Payload` that follows this header.   |
| 8+     | Variable     | Actual Data Payload   | The command-specific success data. Its length is `Payload Length`.        |

**2. Error Response Structure (Total Header: 12 bytes)**

| Offset | Size (bytes) | Field                 | Description                                                                 |
|--------|--------------|-----------------------|-----------------------------------------------------------------------------|
| 0      | 1            | Response Type         | `0x01` (Indicates Error)                                                    |
| 1      | 1            | Original Command ID   | The command ID from the original request.                                   |
| 2-3    | 2            | Reserved              | `0x00, 0x00` (For alignment and future use)                               |
| 4-7    | 4            | Error Message Length  | Big-endian. Length of the `Error Message String` that follows the Error Code. |
| 8-11   | 4            | Error Code            | Big-endian. A specific numeric code identifying the type of error.          |
| 12+    | Variable     | Error Message String  | ASCII string detailing the error. Its length is `Error Message Length`. Not null-terminated. |

**3. Progress Response Structure**

Progress messages use a 16-byte header followed by a 4-byte message-length field and the ASCII status string:

| Offset | Size (bytes) | Field               | Description                                                                 |
|--------|--------------|---------------------|-----------------------------------------------------------------------------|
| 0      | 1            | Response Type       | `0x02` (Indicates Progress Update)                                          |
| 1      | 1            | Original Command ID | The command ID from the original request.                                   |
| 2-3    | 2            | Reserved            | `0x00, 0x00` (Alignment and future use)                                     |
| 4-7    | 4            | Payload Length      | Big-endian. Size of the bytes **after** the header (message length field + status message). |
| 8-11   | 4            | Current Step        | Big-endian. Current progress step (1-based).                                |
| 12-15  | 4            | Total Steps         | Big-endian. Total number of steps in the operation.                         |
| 16-19  | 4            | Status Length       | Big-endian. Length of the status message that follows (not null-terminated).|
| 20+    | Variable     | Status Message      | ASCII status string with `Status Length` bytes.                             |

**Progress Response Usage:**
- Sent during long-running operations (Optotune calibration, logging operations, MSGDMA control)
- Multiple progress responses may be emitted for a single command
- Always followed by a final Success or Error response when the operation completes
- Clients should parse the `Status Length` field after reading the 16-byte header, then read the indicated number of bytes for the message
- `Current Step` and `Total Steps` provide completion percentage: `(Current Step / Total Steps) * 100`

---

## Protocol Command List
- **Command 1:** Write (32-bit values)
- **Command 2:** Read
- **Command 3:** Generic Control commands
- **Command 4:** Optotune commands
- **Command 5:** MSGDMA streaming commands
- **Command 6:** Reserved
- **Command 7:** I2C write
- **Command 8:** I2C read
- **Command 9:** Ping/Echo (Connection Test)
- **Command 10:** SPI Register Write
- **Command 11:** AWG DAC Vector Transfer
- **Command 12:** Reserved
- **Command 13:** ~~Optotune Set Frequency~~ (Deprecated - use Command 4, Sub-commands 12 and 13)

---

## Error Codes

The following error codes may be returned in error responses:

| Error Code 					| Value      | Description                                    										|
|-------------------------------|------------|--------------------------------------------------------------------------------------|
| SUCCESS    					| 0x00000000 | Operation completed successfully               										|
| GENERAL_ERROR 				| 0x00000001 | General/unspecified error (used for directory creation failures, SPI errors, etc.) 	|
| INVALID_COMMAND 				| 0x00000002 | Unknown or unsupported command             											|
| INVALID_PARAMETERS 			| 0x00000003 | Invalid command parameters               											|
| I2C_DEVICE_OPEN_FAILED 		| 0x00000004 | Failed to open I2C device             												|
| I2C_TRANSACTION_FAILED 		| 0x00000005 | I2C transaction failed                 												|
| PHYSICAL_MEMORY_ACCESS_FAILED | 0x00000006 | Physical memory access failed      													|
| INVALID_COMMAND_ID 			| 0x00000007 | Invalid command ID                     												|
| PATTERN_FILES_NOT_FOUND 		| 0x00000008 | Pattern files not found (Optotune Run with Pattern sub-command 9) 					|
| OPTOTUNE_HARDWARE_FAILURE		| 0x00000009 | Optotune hardware operation failed (sub-commands 1, 2, 3, 5) 						|
| OPTOTUNE_LOGGING_FAILURE 		| 0x0000000A | Optotune data logging operation failed (sub-commands 4, 9) 							|

---

## Protocol Command Details

### Command 1: Write (32-bit values)
- **Purpose:** Write 32-bit data to a specified address or register.
- **Format:**
  - Byte 0: Command (1)
  - Byte 1: Index/ID (typically 0)
  - Bytes 2-5: Address (32-bit, big-endian)
  - Bytes 6-9: Length (number of 32-bit values, big-endian, set to 1 for single word)
  - Bytes 10+: Data payload (32-bit values, big-endian)
- **Total packet length:** 10 bytes header + (length × 4) bytes data
- **Usage:** Used to write configuration or control values to the FPGA or peripherals.

### Command 2: Read
- **Purpose:** Read 32-bit data from a specified address or register.
- **Format:**
  - Byte 0: Command (2)
  - Byte 1: Index/ID (typically 0)
  - Bytes 2-5: Address (32-bit, big-endian)
  - Bytes 6-9: Length (number of 32-bit values to read, big-endian)
  - No data payload in request
- **Total packet length:** 10 bytes header only
- **Response:**
  - The response is prefixed by the 8-byte **Success Response Header**.
  - Following the header:
    - Data payload: (length × 4) bytes of 32-bit values (big-endian)
- **Usage:** Used to read status or configuration from the FPGA or peripherals.

### Command 3: Generic Control Commands
- **Purpose:** Issue basic control operations (reset).
- **Format:**
  - Byte 0: Command (3)
  - Byte 1: Control code/ID (see table below)
  - Bytes 2-5: Address (typically 0, reserved)
  - Bytes 6-9: Length (typically 0, reserved)
  - No data payload
- **Total packet length:** 10 bytes header only
- **Usage:** Used for basic system control operations.

#### Command 3 Control IDs

| ID | Name 		| Description 										| Response Payload 						|
|----|--------------|---------------------------------------------------|---------------------------------------|
| 1  | Reset MCU 	| Stop sampling, exit sending task, and reset MCU 	| None 									|
| 2  | Get Version 	| Get application version information 				| Version string (null-terminated ASCII)|

- **Response:** 
  - **ID 1 (Reset MCU):** Success response with no payload
  - **ID 2 (Get Version):** Success response with null-terminated version string as payload
    - Format: `"major.minor (timestamp)"` (e.g., `"0.97 (2025-10-21 14:30:45)"`)
    - The timestamp reflects the build time from CMakeLists.txt
    - Payload length includes the null terminator

**Example Client Usage for Get Version (Sub-command 2):**
```python
import socket
import struct

# Connect to ODEM application
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(('192.168.2.24', 24871))

# Send Get Version request (Command 3, Sub-command 2)
get_version_packet = bytes([
    0x03,  # Command 3 (Generic Control)
    0x02,  # Sub-command 2 (Get Version)
    0x00, 0x00, 0x00, 0x00,  # Address (unused)
    0x00, 0x00, 0x00, 0x00   # Length (unused)
])
sock.send(get_version_packet)

# Receive response
response = sock.recv(1024)

# Parse success response header (8 bytes)
response_type = response[0]  # Should be 0x00 (Success)
command_id = response[1]      # Should be 0x03 (Command 3)
reserved = response[2:4]      # Reserved bytes
payload_length = struct.unpack('>I', response[4:8])[0]  # Big-endian uint32

if response_type == 0x00:  # Success
    # Extract version string (null-terminated)
    version_string = response[8:8+payload_length].decode('ascii').rstrip('\x00')
    print(f"Application Version: {version_string}")
    # Example output: "Application Version: 0.97 (2025-10-21 14:30:45)"
else:
    print(f"Error: Unexpected response type {response_type}")

sock.close()
```

**Example Request/Response Bytes:**
```
Request (10 bytes):
  03 02 00 00 00 00 00 00 00 00

Response (example with version "0.97 (2025-10-21 14:30:45)"):
  Header (8 bytes):
    00                    - Response Type: Success
    03                    - Original Command ID: 3
    00 00                 - Reserved
    00 00 00 1D           - Payload Length: 29 bytes (28 chars + null terminator)
  
  Payload (29 bytes):
    30 2E 39 37 20 28 32  - "0.97 (2"
    30 32 35 2D 31 30 2D  - "025-10-"
    32 31 20 31 34 3A 33  - "21 14:3"
    30 3A 34 35 29 00     - "0:45)" + null terminator
```

### Command 4: Optotune Commands
- **Purpose:** Control Optotune mirror operations.
- **Format:**
  - Byte 0: Command (4)
  - Byte 1: Sub-command ID (see table below)
  - Bytes 2-5: Address (typically 0, reserved)
  - Bytes 6-9: Length (payload length for commands that require parameters)
  - Optional payload for certain commands
- **Total packet length:** 10 bytes header + optional payload
- **Usage:** Used for Optotune mirror control and logging operations.

#### Command 4 Sub-Command IDs

| ID | Name 						  | Description 										  | Payload Required | Progress Reporting |
|----|--------------------------------|-------------------------------------------------------|------------------|-------------------|
| 1  | Run 				 		      | Start Optotune operation with optional mode parameter | Optional (1 byte mode) | Yes - 5 steps |
| 2  | Run with Calibration 		  | Start Optotune operation with calibration 			  | No 					   | Yes - 7 steps (setup + 5 iterations + completion) |
| 3  | Stop 				 		  | Stop Optotune operation 							  | No 					   | No |
| 4  | Acquire Log 		 		      | Acquire Optotune logger data 						  | No | No |
| 5  | Run with Adaptive Calibration  | Start Optotune with adaptive calibration 			  | No | Yes - Variable steps |
| 6  | SPI Logging Control 		      | Control SPI logging to file 						  | Yes (1 byte action) | No |
| 7  | SPI Register Read 			  | Direct read of Optotune SPI register 				  | Yes (2 bytes: system_id, register_id) | No |
| 8  | SPI Register Write 			  | Direct write to Optotune SPI register 				  | Yes (6 bytes: system_id, register_id, value) | No |
| 9  | Run with Pattern 			  | Start Optotune with existing pattern files 			  | Yes (pattern identifier string) | Yes - 5 steps with file paths |
| 10 | Reserved 					  | Reserved for future use 	   						  | - | - |
| 11 | Write LUT from Last Log 	      | Write LOCATION_LUT using last measured log data 	  | Optional (8 bytes: uint32_t optotune_points, uint32_t fpga_points) | No |
| 12 | Set VPU and Logger Frequencies | Set both VPU and Logger frequencies 				  | Yes (8 bytes: 2 IEEE 754 floats) | No |
| 13 | Get VPU and Logger Frequencies | Read current VPU and Logger frequencies 			  | No | No |

**Command 4 Error Handling:**
- **Optotune Hardware Failures:** Sub-commands 1, 2, 3, 5 use `ERROR_CODE_OPTOTUNE_HARDWARE_FAILURE` (0x00000009) for hardware-related failures
- **Optotune Logging Failures:** Sub-commands 4, 9 use `ERROR_CODE_OPTOTUNE_LOGGING_FAILURE` (0x0000000A) for data logging operation failures  
- **Pattern Files Not Found:** Sub-command 9 specifically uses `ERROR_CODE_PATTERN_FILES_NOT_FOUND` (0x00000008) when pattern files cannot be located
- **General Failures:** Directory creation failures and other non-specific errors use `ERROR_CODE_GENERAL_ERROR` (0x00000001)
- **Error Message Differentiation:** Each error type provides specific error messages for better client handling:
  - Pattern files not found: `"Pattern files not found for the specified identifier"`
  - Hardware failures: `"Optotune [operation] operation failed"`, `"Failed to open Optotune device"`
  - Logging failures: `"Optotune data logging operation failed"`, `"Optotune log acquisition failed"`
  - Directory creation failures: `"Failed to create pattern results directory"`

**Optotune Run Mode Parameter (Sub-command 1):**
- **0:** Use preset files
- **1:** Generate test pattern (default if no payload provided)

**Optotune SPI Logging Control Parameter (Sub-command 6):**
- **0:** Stop SPI logging
- **1:** Start SPI logging  
- **2:** Get SPI logging status

**Optotune SPI Register Read Parameter (Sub-command 7):**
- **Payload:** 2 bytes total
  - **Byte 0:** System ID (0x00-0xFF) - Optotune internal system identifier
  - **Byte 1:** Register ID (0x00-0xFF) - Register address within the system
- **Response:** Success response with 4-byte payload containing the 32-bit register value (big-endian)
- **Usage:** Directly read Optotune device registers for debugging or monitoring
- **Example:** Read firmware version register (system=0x10, register=0x03)

**Optotune SPI Register Write Parameter (Sub-command 8):**
- **Payload:** 6 bytes total
  - **Byte 0:** System ID (0x00-0xFF) - Optotune internal system identifier  
  - **Byte 1:** Register ID (0x00-0xFF) - Register address within the system
  - **Bytes 2-5:** Value (32-bit, big-endian) - Value to write to the register
- **Response:** Success response with no payload
- **Usage:** Directly write Optotune device registers for configuration or control
- **Example:** Write control register (system=0x01, register=0x05, value=0x12345678)

**Optotune Run with Pattern Parameter (Sub-command 9):**
- **Payload:** Variable length (1-255 bytes)
  - **Pattern Identifier:** Null-terminated string identifying the pattern files to use
- **Response:** Success response with no payload
- **Usage:** Run Optotune mirror with existing pattern files stored in `/data/app/odem/calibration/`
- **Pattern File Naming Conventions** (searches in order):
  1. `{pattern_id}_waveformX.csv` and `{pattern_id}_waveformY.csv`
  2. `waveformX_{pattern_id}.csv` and `waveformY_{pattern_id}.csv`  
  3. `pattern{pattern_id}_X.csv` and `pattern{pattern_id}_Y.csv`
- **Examples:** 
  - Pattern ID `"test1"` → searches for `test1_waveformX.csv` and `test1_waveformY.csv`
  - Pattern ID `"1"` → searches for `1_waveformX.csv` and `1_waveformY.csv`, or `pattern1_X.csv` and `pattern1_Y.csv`
  - Pattern ID `"circle"` → searches for `circle_waveformX.csv` and `circle_waveformY.csv`

**Optotune Write LUT from Last Log (Sub-command 11):**
- **Payload:** Optional 8 bytes (if not provided, both parameters default to 0 for auto-calculation)
  - **Bytes 0-3:** OptoTune points (32-bit unsigned integer, big-endian) - Number of OptoTune logger samples to use (0 = auto-calculate)
  - **Bytes 4-7:** FPGA points (32-bit unsigned integer, big-endian) - Number of FPGA sample points for LUT (0 = auto-calculate)
- **Response:** Success response with no payload
- **Usage:** Automatically writes calibrated position data to FPGA LOCATION_LUT memory using the most recent Optotune log measurement
- **Prerequisites:** Must have previously run Optotune Acquire Log (sub-command 4) or other logging operation that generates `measured_xp.csv` and `measured_yp.csv` files
- **Example Payloads:**
  - **No parameters (auto-calculate):** No payload bytes, field length = 0
  - **OptoTune=50 points, FPGA=2048 points:** `00 00 00 32 00 00 08 00` (8 bytes, big-endian)
  - **OptoTune=auto, FPGA=4096 points:** `00 00 00 00 00 00 10 00` (8 bytes, optotune_points=0 for auto)
  - **OptoTune=100 points, FPGA=auto:** `00 00 00 64 00 00 00 00` (8 bytes, fpga_points=0 for auto)
- **Error Conditions:**
  - Missing measured data files: Returns `ERROR_CODE_OPTOTUNE_LOGGING_FAILURE` (0x0000000A)
  - Invalid logger frequency (when auto-calculating): Returns `ERROR_CODE_OPTOTUNE_HARDWARE_FAILURE` (0x00000009)
  - Interpolation failure: Returns `ERROR_CODE_GENERAL_ERROR` (0x00000001)
  - LUT write failure: Returns `ERROR_CODE_GENERAL_ERROR` (0x00000001)
- **Detailed Documentation:** See `docs/OPTOTUNE_CONTROL.md` for complete processing algorithm, parameter override use cases, console output examples, and key constants

**Optotune Set VPU and Logger Frequencies (Sub-command 12):**
- **Payload:** 8 bytes total
  - **Bytes 0-3:** VPU frequency (32-bit IEEE 754 float, big-endian) - Hz
  - **Bytes 4-7:** Logger frequency (32-bit IEEE 754 float, big-endian) - Hz
- **Response:** Success response with no payload
- **Usage:** Configure the frequencies used by Optotune run and logger operations
- **Frequency Assignment:**
  - **VPU frequency:** Used for waveform scanning rate in `optotune_run` operations
  - **Logger frequency:** Used for data acquisition sampling rate in logger operations
- **Frequency Range (for both frequencies):**
  - **Minimum:** 40000.0 / (2³² - 1) ≈ 9.313e-6 Hz (theoretical lower bound)
  - **Maximum:** 40000.0 Hz (Optotune MR-E-3 frame rate limit)
  - **Practical Range:** 0.1 Hz to 40000.0 Hz recommended
- **Default:** 40000.0 Hz (set at startup for both VPU and Logger)
- **Validation:**
  - Returns `ERROR_CODE_INVALID_PARAMETERS` (0x00000003) if field_length ≠ 2
  - Returns `ERROR_CODE_INVALID_PARAMETERS` (0x00000003) if any frequency ≤ 0 or > 40000.0 Hz
  - Returns `ERROR_CODE_INVALID_PARAMETERS` (0x00000003) if invalid IEEE 754 float format
- **Takes effect immediately:** Changes apply to subsequent Optotune operations
- **Example Payload:**
  - **VPU=3000.0 Hz, Logger=1500.0 Hz:** `45 3B 80 00 44 BB 80 00` (8 bytes)
  - **VPU=5000.0 Hz, Logger=5000.0 Hz:** `45 9C 40 00 45 9C 40 00` (8 bytes)

**Optotune Get VPU and Logger Frequencies (Sub-command 13):**
- **Payload:** None
- **Response:** Success response with 8-byte payload
  - **Bytes 0-3:** Current VPU frequency (32-bit IEEE 754 float, big-endian) - Hz
  - **Bytes 4-7:** Current Logger frequency (32-bit IEEE 754 float, big-endian) - Hz
- **Usage:** Query the current frequencies configured for Optotune operations
- **Read-only:** Does not modify any settings
- **Returns:** Current frequency values that will be used by subsequent operations
- **Example Response Payload:**
  - **VPU=40000.0 Hz, Logger=40000.0 Hz:** `47 1C 40 00 47 1C 40 00` (8 bytes)
  - **VPU=3000.0 Hz, Logger=1500.0 Hz:** `45 3B 80 00 44 BB 80 00` (8 bytes)

**Command 4 Frequency Control Migration:**
- **Deprecated:** Command 13 (Optotune Set Frequency) is deprecated in favor of sub-commands 12 and 13
- **Advantages of Sub-commands:**
  - **Read Capability:** Sub-command 13 allows querying current frequencies (not possible with Command 13)
  - **Consistent Structure:** Frequencies are grouped under Optotune command structure
  - **Better Organization:** All Optotune operations unified under Command 4
- **Backward Compatibility:** Command 13 may be removed in future versions - clients should migrate to sub-commands 12 and 13

**Missing Pattern Files Error Handling:**
When pattern files are not found, the command returns a specific error response:
- **Error Code:** `0x00000008` (`ERROR_CODE_PATTERN_FILES_NOT_FOUND`) - **DEDICATED ERROR CODE**
- **Error Message:** `"Pattern files not found for the specified identifier"`
- **Specificity:** This error code is exclusively used for missing pattern files in Optotune Run with Pattern (sub-command 9)
- **Other Sub-command 9 Failures:** Different error codes for other failure types:
  - Hardware failures: `ERROR_CODE_OPTOTUNE_HARDWARE_FAILURE` (0x00000009) - `"Optotune hardware operation failed"`
  - Device access failures: `ERROR_CODE_OPTOTUNE_HARDWARE_FAILURE` (0x00000009) - `"Failed to open Optotune device"`
  - Logging failures: `ERROR_CODE_OPTOTUNE_LOGGING_FAILURE` (0x0000000A) - `"Optotune data logging operation failed"`
  - Directory creation failures: `ERROR_CODE_GENERAL_ERROR` (0x00000001) - `"Failed to create pattern results directory"`
**Detailed Error Logging:** The system logs a detailed message showing:
  - `"ERROR: Pattern files not found for identifier '{pattern_id}'">`
  - All attempted file naming patterns that were searched
  - Expected file locations in `/data/app/odem/calibration/`

**Example Missing Files Error Response:**
```
Response Type: 0x01 (Error)
Error Code: 0x00000008 (PATTERN_FILES_NOT_FOUND)  
Error Message: "Pattern files not found for the specified identifier"

Console Output (if verbose enabled):
ERROR: Pattern files not found for pattern ID 'missing_pattern' in /data/app/odem/calibration/
Tried the following naming patterns:
  - missing_pattern_waveformX.csv and missing_pattern_waveformY.csv
  - waveformX_missing_pattern.csv and waveformY_missing_pattern.csv
  - patternmissing_pattern_X.csv and patternmissing_pattern_Y.csv
```

**Progress Reporting with File Paths:**
This command provides detailed progress reporting (5 steps total) with extractable file paths:
- **Step 1/5:** `"Pattern discovery and setup"`
- **Step 2/5:** `"Running with pattern files - Directory: {output_directory_path}"`
- **Step 3/5:** `"Calculating scan timing and waiting"`
- **Step 4/5:** `"Acquiring measurement log - Files: {pattern_id}_log_*"`
- **Step 5/5:** `"Optotune run with pattern completed - Results: {output_directory_path}"`

**Extracting File Paths from Progress Messages:**
Clients can extract file paths from progress messages by parsing the following patterns:
- **Output Directory:** Extract path after `"Directory: "` in Step 2/5 progress message
- **Results Directory:** Extract path after `"Results: "` in Step 5/5 progress message  
- **Log File Prefix:** Extract prefix from `"Files: "` in Step 4/5 (append `.csv` extensions)

**Generated Files Structure:**
The operation creates a timestamped directory with the following structure:
```
{ODEM_DATA_DIR}/pattern_results/{pattern_id}_log_{timestamp}/
├── {pattern_id}_log_xpos.csv         # X-axis position measurements
├── {pattern_id}_log_ypos.csv         # Y-axis position measurements
├── {pattern_id}_log_measured.csv     # Combined measurement data
├── {pattern_id}_cycle_xpos.csv       # Single cycle X-axis data
├── {pattern_id}_cycle_ypos.csv       # Single cycle Y-axis data
└── {pattern_id}_cycle_measured.csv   # Single cycle combined data
```

**Example Path Extraction:**
```python
# Progress message example: "Running with pattern files - Directory: /var/lib/odem/pattern_results/test1_log_20250909_143022"
if "Directory: " in progress_message:
    output_dir = progress_message.split("Directory: ")[1].strip()
    
# Progress message example: "Optotune run with pattern completed - Results: /var/lib/odem/pattern_results/test1_log_20250909_143022"
if "Results: " in progress_message:
    results_dir = progress_message.split("Results: ")[1].strip()
    # Files available: test1_log_*.csv and test1_cycle_*.csv in results_dir
```

**Progress Reporting Details:**
- **Optotune Run (Sub-command 1):** Reports progress through 5 steps:
  1. "Initializing mirror and VPU configuration"
  2. "Loading waveform data"
  3. "Starting mirror axes and logger synchronization"
  4. "Running measurement and data acquisition"
  5. "Saving measurement data and completing operation"

- **Optotune Run with Calibration (Sub-command 2):** Reports progress through 7 steps:
  1. "Setting up calibration environment"
  2. "Calibration iteration 1/5"
  3. "Calibration iteration 2/5"
  4. "Calibration iteration 3/5"
  5. "Calibration iteration 4/5"
  6. "Calibration iteration 5/5"
  7. "Finalizing calibration results"

- **Response:** 
  - **Success:** Success response with no payload for most Optotune commands, except sub-command 7 (SPI Register Read) which returns a 4-byte register value.
  - **Error:** Specific error codes based on failure type:
    - Sub-commands 1, 2, 3, 5: `ERROR_CODE_OPTOTUNE_HARDWARE_FAILURE` (0x00000009)
    - Sub-command 4: `ERROR_CODE_OPTOTUNE_LOGGING_FAILURE` (0x0000000A)
    - Sub-command 9: `ERROR_CODE_PATTERN_FILES_NOT_FOUND` (0x00000008) or `ERROR_CODE_OPTOTUNE_LOGGING_FAILURE` (0x0000000A)
    - Directory/general failures: `ERROR_CODE_GENERAL_ERROR` (0x00000001)

**Optotune SPI Logging (Sub-command 6) Details:**

The SPI logging feature provides detailed timing analysis of Optotune SPI operations by capturing all SPI transactions to dedicated log files. This is particularly useful for debugging SPI communication issues and analyzing timing performance.

**Log File Location:**
- Base directory: `/data/logs/odem/` (persistent across A/B updates)
- Log directory format: `optotune_spi_YYYYMMDD_HHMMSS/`
- Log file name: `optotune_spi.log`
- Example full path: `/data/logs/odem/optotune_spi_20240815_225854/optotune_spi.log`

**Log Format:**
- High-precision timestamps with microsecond accuracy
- Format: `[YY-MM-DD HH:MM:SS.uuuuuu (+elapsed_us)] [OPTOTUNE-SPI] message`
- Captures: vector operations, register read/write, error conditions, retry attempts

**Usage Notes:**
- SPI logging starts automatically when the application starts
- Can be controlled via protocol command for dynamic start/stop
- Logs are written to both console (if verbose mode enabled) and file simultaneously
- Log files are automatically closed when application exits

### Command 5: MSGDMA Streaming Commands
- **Purpose:** Control MSGDMA (Memory-Mapped to Streaming DMA) operations.
- **Format:**
  - Byte 0: Command (5)
  - Byte 1: Sub-command ID (see table below)
  - Bytes 2-5: Address (typically 0, reserved)
  - Bytes 6-9: Length (typically 0, reserved)
  - No data payload
- **Total packet length:** 10 bytes header only
- **Usage:** Used for high-speed data streaming control.

#### Command 5 Sub-Command IDs

| ID | Name | Description |
|----|------|-------------|
| 1  | Start MSGDMA Streaming | Start MSGDMA continuous streaming with hardware device control |
| 2  | Stop MSGDMA Streaming | Stop MSGDMA continuous streaming and retrieve performance statistics |

**Usage Notes:**
- Sub-command 1 performs device availability check before starting streaming
- Sub-command 2 returns detailed performance statistics in the response
- Requires proper system configuration for hardware access

- **Response:** Success response with no payload for MSGDMA commands.


### Command 7: I2C Write (Extended Format)
- **Purpose:** Write data to an I²C device, supporting muxes, register addressing, and variable data widths.
- **Packet Format:**
| Offset | Size | Field           | Description                                      |
|--------|------|----------------|--------------------------------------------------|
| 0      | 1    | Command code   | 0x07                                             |
| 1      | 1    | Bus            | I²C bus number on the host                       |
| 2      | 1    | Mux address    | I²C-mux upstream address                         |
| 3      | 1    | Mux channel    | Downstream channel number                        |
| 4      | 1    | Device address | 7-bit I²C slave address                          |
| 5      | 1    | Options        | See options byte details below                   |
| 6–9    | 4    | Length (M)     | Number of data words to follow (big-endian)      |
| 10–…   | 0–2  | Register addr  | 0, 1, or 2 bytes, as specified by Reg-Addr-Width |
| …      | M×(W/8) | Data payload | M × (data-width/8) bytes of data                |

- **Total packet length:** 10 header bytes + Reg-Addr-Width + M × (data-width/8)

### Command 8: I2C Read (Extended Format)
- **Purpose:** Read data from an I²C device, supporting muxes, register addressing, and variable data widths.
- **Packet Format:**
| Offset | Size | Field           | Description                                      |
|--------|------|----------------|--------------------------------------------------|
| 0      | 1    | Command code   | 0x08                                             |
| 1      | 1    | Bus            | I²C bus number on the host                       |
| 2      | 1    | Mux address    | I²C-mux upstream address                         |
| 3      | 1    | Mux channel    | Downstream channel number                        |
| 4      | 1    | Device address | 7-bit I²C slave address                          |
| 5      | 1    | Options        | See options byte details below                   |
| 6–9    | 4    | Length (N)     | Number of bytes to read (big-endian)             |
| 10–…   | 0–2  | Register addr  | 0, 1, or 2 bytes, as specified by Reg-Addr-Width |

- **Total packet length:** 10 header bytes + Reg-Addr-Width; then host reads back N bytes.

#### Options Byte Details
| Bits | 7–6      | 5–4            | 3–2           | 1–0         |
|------|----------|----------------|---------------|-------------|
| Name | Reserved | Reg-Addr-Width | Data-Width    | Speed       |

- **Reg-Addr-Width (bits 5–4):**
  - 00 = none
  - 01 = 8-bit pointer
  - 10 = 16-bit pointer
  - 11 = reserved
- **Data-Width Code (bits 3–2):**
  - 00 = 8-bit data
  - 01 = 16-bit data
  - 10, 11 = reserved
- **Speed Code (bits 1–0):**
  - 00 = Standard-mode ≤ 100 kbit/s
  - 01 = Fast-mode ≤ 400 kbit/s
  - 10, 11 = reserved

This uses 6 bits for your three fields and leaves bits 7–6 free for future flags (e.g. 10-bit addressing or PEC).

#### Example Packets
- **DAC63202 (Channel 3, I²C 0x4B) Write DAC-X-DATA 0x19**
  - `07 00 70 03 4B 14 00 00 00 01 19 FF F0`
    - 07 = CMD_I2C_WRITE
    - 00 = I²C bus 0
    - 70 = MUX address
    - 03 = MUX channel
    - 4B = device address
    - 14 = Options: Reg-Addr-Width=01 (8 bit) · Data-Width=01 (16 bit) · Speed=00 (100 kHz)
    - 00 00 00 01 = length = 1 word (2 bytes)
    - 19 = register = DAC-1-DATA (I²C addr 0x19)
    - FF F0 = 12-bit full-scale, left-aligned in 16 bits

- **ADS7828 (Channel 4, I²C 0x48) Write pointer to start CH2 conversion (command = 0xC2)**
  - `07 00 70 04 48 10 00 00 00 00 C2`
    - 10 = Options: Reg-Addr=01·Data-Width=00 (8 bit)·Speed=00
    - 00 00 00 00 = length 0 → no post-payload
    - C2 = command/pointer byte

- **Read conversion result (1×16-bit word)**
  - `08 00 70 04 48 04 00 00 00 01`
    - 04 = Options: Reg-Addr=00 (Don’t write any register after the address)·Data-Width=01 (16 bit)·Speed=00
    - 00 00 00 01 = length 1 word
    - → slave returns 2 bytes: MSB (D11–D4), then LSB (D3–D0 padded)

### Command 9: Ping/Echo (Connection Test)
- **Purpose:** Simple connectivity test to verify the TCP connection is alive.
- **Format:**
  - Byte 0: Command (9)
  - Byte 1: ID (unused, can be 0)
  - Bytes 2-5: Address (unused, can be 0)
  - Bytes 6-9: Field Length (unused, can be 0)
- **Response:**
  - The response is prefixed by the 8-byte **Success Response Header**.
  - `Payload Length` in the header will be 0.
  - There is no data payload following the header for a Ping response.
- **Usage:** 
  - Used by clients to periodically test connection health
  - Can be sent at regular intervals to keep TCP connection alive
  - Provides a simple way to measure round-trip time
  - No side effects on system operation

### Command 10: SPI Register Write
- **Purpose:** Write a single 32-bit value to an SPI register with configurable effective bits.
- **Format:**
  - Byte 0: Command (10)
  - Byte 1: Sub-device Address (0-255, used as SPI sub-device identifier)
  - Bytes 2-5: Address (reserved, set to 0)
  - Bytes 6-9: Field Length (number of effective bits: 0-22, where 0 means 22 bits, big-endian)
  - Bytes 10-13: Value (32-bit value to write, big-endian)
- **Total packet length:** 14 bytes (10 header + 4 payload)
- **Response:**
  - The response is prefixed by the 8-byte **Success Response Header**.
  - `Payload Length` in the header will be 0.
  - There is no data payload following the header for a successful write.
- **Usage:** 
  - Used to configure SPI-connected devices (e.g., DACs, ADCs)
  - The `Field Length` parameter specifies how many bits of the 32-bit value are significant
  - Value 0 in `Field Length` defaults to 22 bits (common for many SPI devices)
  - The implementation validates that the value fits within the specified bit range
- **Error Conditions:**
  - Returns `ERROR_CODE_GENERAL_ERROR` if `Field Length` > 22
  - Returns `ERROR_CODE_GENERAL_ERROR` if `Value` exceeds the maximum for the specified number of bits
  - Returns `ERROR_CODE_GENERAL_ERROR` if SPI communication fails
- **Implementation Details:**
  - Maximum effective bits: 22 (defined by `DRIVERS_FPGA_MAX_SPI_EFFECTIVE_BITS`)
  - Value validation: For N bits, maximum value is (2^N - 1)
  - Calls internal function `spi_write_register_32()` for actual SPI communication

### Command 11: AWG DAC Vector Transfer
- **Purpose:** Transfer a vector of DAC values to the AWG (Arbitrary Waveform Generator) via SPI.
- **Format:**
  - Byte 0: Command (11)
  - Byte 1: Sub-device Address (0-7, DAC sub-device identifier)
  - Bytes 2-5: Reserved (set to 0)
  - Bytes 6-9: Field Length (number of 16-bit DAC values, big-endian)
  - Bytes 10+: DAC Values (field_length × 2 bytes of 16-bit DAC values, big-endian)
- **Total packet length:** 10 bytes header + (field_length × 2) bytes data
- **Response:**
  - The response is prefixed by the 8-byte **Success Response Header**.
  - `Payload Length` in the header will be 0.
  - There is no data payload following the header for a successful transfer.
- **Usage:** 
  - Used to upload waveform data to DAC devices for arbitrary waveform generation
  - Each DAC value is 16 bits (allowing for high-resolution waveforms)
  - Sub-device address allows targeting specific DAC channels (0-7)
- **Error Conditions:**
  - Returns `ERROR_CODE_GENERAL_ERROR` if SPI communication fails
  - Returns `ERROR_CODE_GENERAL_ERROR` if sub-device address is invalid
- **Implementation Details:**
  - Calls internal function `spi_send_dac_vector()` for actual SPI communication
  - Supports vectorized transfer for efficient bulk data upload
  - Big-endian byte order for both header fields and DAC values

### Command 13: Optotune Set Frequency (DEPRECATED)

**⚠️ DEPRECATED:** This command is deprecated as of protocol version 2.1. Please use Command 4, Sub-commands 12 and 13 instead for frequency control with read capability.

- **Purpose:** Configure the frequencies used by Optotune run and acquire log operations.
- **Migration Path:** 
  - **Setting frequencies:** Use Command 4, Sub-command 12 (Optotune Set VPU and Logger Frequencies)
  - **Reading frequencies:** Use Command 4, Sub-command 13 (Optotune Get VPU and Logger Frequencies)
- **Format:**
  - Byte 0: Command (13)
  - Byte 1: Index/ID (unused, typically 0)
  - Bytes 2-5: Address (unused, set to 0)
  - Bytes 6-9: Field Length (must be 2, big-endian)
  - Bytes 10-13: VPU frequency value (32-bit IEEE 754 float, big-endian)
  - Bytes 14-17: Logger frequency value (32-bit IEEE 754 float, big-endian)
- **Total packet length:** 18 bytes
- **Frequency Assignment:**
  - **First value:** VPU scanning frequency (used in `optotune_run`)
  - **Second value:** Logger frequency (used in `optotune_acquire_log` and mirror initialization)
- **Frequency Range (for both frequencies):** 
  - **Minimum:** 40000.0 / (2³² - 1) ≈ 9.313e-6 Hz (theoretical lower bound)
  - **Maximum:** 40000.0 Hz (Optotune MR-E-3 frame rate limit)
  - **Practical Range:** 0.1 Hz to 40000.0 Hz recommended
- **Default:** 40000.0 Hz (set at startup for both VPU and Logger)
- **Response:**
  - The response is prefixed by the 8-byte **Success Response Header**.
  - `Payload Length` in the header will be 0.
  - There is no data payload following the header for a successful operation.
- **Error Conditions:**
  - Returns `ERROR_CODE_INVALID_PARAMETERS` if field_length ≠ 2
  - Returns `ERROR_CODE_INVALID_PARAMETERS` if any frequency ≤ 0 or > 40000.0 Hz
  - Returns `ERROR_CODE_INVALID_PARAMETERS` if invalid IEEE 754 float format
- **Usage:** 
  - Used to dynamically adjust frequencies for Optotune mirror operations
  - VPU frequency affects waveform scanning in `optotune_run`
  - Logger frequency affects sampling rate in `optotune_acquire_log` and mirror initialization
  - Takes effect immediately for subsequent operations
- **Example:** 
  - **VPU=3000.0 Hz, Logger=1500.0 Hz:** `0D 00 00 00 00 00 00 00 00 02 45 3B 80 00 44 BB 80 00`
- **Limitations:**
  - **Write-only:** Cannot query current frequency settings (use Sub-command 13 for read capability)
  - **Less organized:** Not grouped with other Optotune operations under Command 4

**Recommended Migration:**
```python
# OLD: Command 13 (deprecated, write-only)
old_packet = bytes([0x0D, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x02]) + \
             struct.pack('>ff', vpu_freq, logger_freq)

# NEW: Command 4, Sub-command 12 (recommended, same write functionality)
new_packet = bytes([0x04, 0x0C, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x02]) + \
             struct.pack('>ff', vpu_freq, logger_freq)

# BONUS: Command 4, Sub-command 13 (read current frequencies - NEW capability)
read_packet = bytes([0x04, 0x0D, 0x00, 0x00, 0x00, 0x00,
                     0x00, 0x00, 0x00, 0x00])
# Response includes 8-byte payload with current VPU and Logger frequencies
```

---

## Vector Support with Simple Data Commands

### Vector Write with CMD_WRITE
The protocol supports writing a vector (array) of 32-bit values to a memory address using the simple write command (CMD_WRITE = 0x01). This is used by the control GUI for efficient bulk data transfer.

**Packet Structure:**
- Byte 0: Command code (0x01)
- Byte 1: Index (for multi-buffer or multi-channel, typically 0)
- Byte 2-5: Memory address (32-bit, big-endian)
- Byte 6-9: Vector length (number of 32-bit values, big-endian)
- Byte 10+: Data payload (vector of 32-bit values, big-endian)

**Total packet length:** 10 bytes header + (length × 4) bytes data

**Example:**
To write a vector of N 32-bit values to a memory address:
```
| 0x01 | idx | addr[4] | length[4] | data[0] | data[1] | ... | data[N-1] |
```
- `0x01` = CMD_WRITE
- `idx` = index (usually 0)
- `addr[4]` = 4 bytes, target memory address (big-endian)
- `length[4]` = 4 bytes, number of 32-bit values (big-endian)
- `data[i]` = 4 bytes per value, big-endian

**Usage:**
- Used for bulk memory writes, such as uploading a waveform or configuration vector to the FPGA.
- The control GUI (see `tabVector.py`) builds and sends this packet when the user selects a vector file and initiates a write.

**Notes:**
- The receiving firmware must interpret the length field and process the following data as a vector.

---

### MSGDMA Protocol Commands (Command 5)

The MSGDMA commands provide runtime control over MSGDMA (Memory-Mapped to Streaming DMA) hardware operations:

| Sub-Command ID | Name | Description |
|----------------|------|-------------|
| 1 | Start MSGDMA Streaming | Start MSGDMA continuous streaming with hardware device control |
| 2 | Stop MSGDMA Streaming | Stop MSGDMA continuous streaming and retrieve performance statistics |

**Usage Notes:**
- Sub-command 1 performs device availability check before starting streaming
- Sub-command 2 returns detailed performance statistics in the response
- Requires proper system configuration for hardware access

**Example Client Usage:**
```python
# Start MSGDMA streaming (Command 5, Sub-command 1)
start_packet = bytes([0x05, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00])
sock.send(start_packet)
response = sock.recv(1024)
# Check response for success (0x00) or error (0x01)

# Stop MSGDMA streaming and get performance stats (Command 5, Sub-command 2)  
stop_packet = bytes([0x05, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00])
sock.send(stop_packet)
response = sock.recv(1024)
# Response includes performance statistics
```

---

### Optotune Protocol Commands (Command 4)

The Optotune commands are specifically designed for Optotune mirror operations:

| Sub-Command ID | Name | Description | Payload | Progress Reporting |
|----------------|------|-------------|---------|-------------------|
| 1 | Optotune Run | Start Optotune operation with optional mode parameter | Optional 1-byte mode | Yes - 5 steps |
| 2 | Optotune Run with Calibration | Start Optotune operation with calibration | None | Yes - 7 steps |
| 3 | Optotune Stop | Stop Optotune operation | None | No |
| 4 | Optotune Acquire Log | Acquire Optotune logger data | None | No |
| 5 | Optotune Run with Adaptive Calibration | Start Optotune with adaptive calibration | None | Yes - Variable steps |
| 6 | Optotune SPI Logging Control | Control SPI logging to file | 1-byte action | No |
| 7 | Optotune SPI Register Read | Direct read of Optotune SPI register | 2 bytes (system_id, register_id) | No |
| 8 | Optotune SPI Register Write | Direct write to Optotune SPI register | 6 bytes (system_id, register_id, value) | No |
| 9 | Optotune Run with Pattern | Start Optotune with existing pattern files | Pattern identifier string | Yes - 5 steps with file paths |

**Example Client Usage:**
```python
# Optotune Run with default mode (Command 4, Sub-command 1, no payload)
run_packet = bytes([0x04, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00])
sock.send(run_packet)

# Handle progress responses and final result
while True:
    response = sock.recv(1024)
    response_type = response[0]
    command_id = response[1]
    
    if response_type == 0x02:  # Progress response
        # Parse progress data
        message_length = int.from_bytes(response[4:8], 'big')
        current_step = int.from_bytes(response[8:10], 'big')
        total_steps = int.from_bytes(response[10:12], 'big')
        status_message = response[16:16+message_length].decode('ascii')
        
        # Display progress
        progress_percent = (current_step / total_steps) * 100
        print(f"Progress: {progress_percent:.1f}% - {status_message}")
        
    elif response_type == 0x00:  # Success response
        print("Optotune operation completed successfully")
        break
        
    elif response_type == 0x01:  # Error response
        error_code = int.from_bytes(response[8:12], 'big')
        print(f"Optotune operation failed with error code: {error_code}")
        break

# Optotune Run with preset files mode (Command 4, Sub-command 1, with 1-byte payload)
run_preset_packet = bytes([0x04, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00])
sock.send(run_preset_packet)

# Optotune Run with Calibration (Command 4, Sub-command 2)
run_cal_packet = bytes([0x04, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00])
sock.send(run_cal_packet)

# Optotune Stop (Command 4, Sub-command 3)
stop_packet = bytes([0x04, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00])
sock.send(stop_packet)

# Optotune Acquire Log (Command 4, Sub-command 4)
log_packet = bytes([0x04, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00])
sock.send(log_packet)

# Optotune Run with Pattern (Command 4, Sub-command 9)
# Example: Run pattern "test1"
pattern_id = "test1".encode('ascii')
field_length = len(pattern_id)
pattern_packet = bytes([0x04, 0x09, 0x00, 0x00, 
                       (field_length >> 24) & 0xFF, (field_length >> 16) & 0xFF, 
                       (field_length >> 8) & 0xFF, field_length & 0xFF,
                       0x00, 0x00]) + pattern_id
sock.send(pattern_packet)

# Handle progress responses with file path extraction
output_directory = None
results_directory = None
log_file_prefix = None

while True:
    response = sock.recv(1024)
    response_type = response[0]
    command_id = response[1]
    
    if response_type == 0x02:  # Progress response
        # Parse progress data
        message_length = int.from_bytes(response[4:8], 'big')
        current_step = int.from_bytes(response[8:10], 'big')
        total_steps = int.from_bytes(response[10:12], 'big')
        status_message = response[16:16+message_length].decode('ascii')
        
        # Extract file paths from specific progress messages
        if "Directory: " in status_message:
            output_directory = status_message.split("Directory: ")[1].strip()
            print(f"Pattern output directory: {output_directory}")
            
        elif "Files: " in status_message:
            log_file_prefix = status_message.split("Files: ")[1].strip().rstrip('*')
            print(f"Log file prefix: {log_file_prefix}")
            
        elif "Results: " in status_message:
            results_directory = status_message.split("Results: ")[1].strip()
            print(f"Pattern results directory: {results_directory}")
            
        # Display progress
        progress_percent = (current_step / total_steps) * 100
        print(f"Progress: {progress_percent:.1f}% - {status_message}")
        
    elif response_type == 0x00:  # Success response
        print("Optotune pattern operation completed successfully")
        print(f"Generated files in: {results_directory}")
        print(f"Log files: {log_file_prefix}*.csv")
        print(f"Cycle files: {pattern_id.decode()}_cycle_*.csv")
        break
        
    elif response_type == 0x01:  # Error response
        error_code = int.from_bytes(response[8:12], 'big')
        error_msg_length = int.from_bytes(response[4:8], 'big')
        error_message = response[12:12+error_msg_length].decode('ascii')
        
        # Check for specific error conditions
        if error_code == 0x00000008:  # PATTERN_FILES_NOT_FOUND
            print(f"Pattern files not found for '{pattern_id.decode()}'")
            print("Check that both X and Y waveform files exist in /data/app/odem/calibration/")
            print("Expected naming patterns:")
            print(f"  - {pattern_id.decode()}_waveformX.csv and {pattern_id.decode()}_waveformY.csv")
            print(f"  - waveformX_{pattern_id.decode()}.csv and waveformY_{pattern_id.decode()}.csv")
            print(f"  - pattern{pattern_id.decode()}_X.csv and pattern{pattern_id.decode()}_Y.csv")
        elif error_code == 0x00000009:  # OPTOTUNE_HARDWARE_FAILURE
            print(f"Optotune hardware failure: {error_message}")
            print("Check Optotune device connection and hardware status")
        elif error_code == 0x0000000A:  # OPTOTUNE_LOGGING_FAILURE
            print(f"Optotune logging failure: {error_message}")
            print("Check data logging permissions and storage availability")
        elif error_code == 0x00000001:  # GENERAL_ERROR (directory creation, etc.)
            if "directory" in error_message.lower():
                print(f"Directory creation failed: {error_message}")
                print("Check file system permissions and available storage space")
            else:
                print(f"Optotune pattern operation failed: {error_message}")
        else:
            print(f"Optotune pattern operation failed with error code: {error_code:08X}")
            print(f"Error message: {error_message}")
        break

# Optotune Run with Numeric Pattern (Command 4, Sub-command 9)  
# Example: Run pattern "1"
pattern_id = "1".encode('ascii')
field_length = len(pattern_id)
pattern_packet = bytes([0x04, 0x09, 0x00, 0x00,
                       (field_length >> 24) & 0xFF, (field_length >> 16) & 0xFF,
                       (field_length >> 8) & 0xFF, field_length & 0xFF,
                       0x00, 0x00]) + pattern_id
sock.send(pattern_packet)
```


---

## Verbose Modes

The protocol implementation includes debug and verbose output modes for troubleshooting and development:

### Verbose Mode
- **Purpose:** Provides detailed information about command processing and data transfer
- **Output:** Command details, data values, transfer progress, memory operations
- **Special Handling:** Ping commands (Command 9) are filtered from verbose output to reduce noise
- **Activation:** Add switch `--verbose` to the program command line


### Progress Indication
- **Write Operations:** Progress bars are displayed for large write operations (>100 values)
- **Format:** `current/total [████████████████████████████████████████████████████████] percentage%`

---

## Implementation Notes

For detailed implementation notes including progress reporting architecture, error handling patterns, endianness considerations, and performance optimization guidance, see:

**📋 [Protocol Implementation Notes](design/PROTOCOL_IMPLEMENTATION_NOTES.md)**

This dedicated document covers:
- **Progress Reporting API Design** - Struct-based configuration and usage patterns
- **Multi-Packet Handling** - Large data transfer implementation
- **Endianness and Data Encoding** - Network byte order and floating-point handling
- **Error Handling Patterns** - Consistent error response structures and dedicated error codes
- **Specific Error Code Usage** - When to use dedicated vs. general error codes
- **Performance Considerations** - TCP optimization and memory management
- **Testing and Debugging** - Debug patterns and validation tools
- **Protocol Evolution Guidelines** - Backward compatibility and extension points

---

## Recent Protocol Enhancements

### Enhanced Error Handling (v2.0)
- **Dedicated Error Codes:** Added specific error codes for precise error detection:
  - `ERROR_CODE_PATTERN_FILES_NOT_FOUND` (0x00000008) - Missing pattern files
  - `ERROR_CODE_OPTOTUNE_HARDWARE_FAILURE` (0x00000009) - Hardware operation failures  
  - `ERROR_CODE_OPTOTUNE_LOGGING_FAILURE` (0x0000000A) - Data logging operation failures
- **Optotune Sub-command Coverage:** All Optotune sub-commands now use specific error codes based on failure type
- **Improved Client Experience:** Clients can reliably detect specific error conditions without parsing error messages
- **Comprehensive Error Mapping:** Each failure type maps to the most appropriate error code for better troubleshooting
- **Backward Compatibility:** Existing error codes and command structures remain unchanged

### Progress Reporting with File Paths
- **Optotune Run with Pattern:** Enhanced progress messages include extractable file paths
- **Directory Tracking:** Progress messages report output directories for result file access
- **File Naming:** Progress messages include file prefixes for generated measurement data
- **Client Integration:** Easy parsing patterns for automated file management workflows

### Pattern File Discovery
- **Multiple Naming Conventions:** Supports three different pattern file naming styles
- **Detailed Error Logging:** Comprehensive console output showing all attempted file locations
- **Flexible Pattern IDs:** Support for both string and numeric pattern identifiers

