# ODEM3 UART Protocol Details

## Overview

This protocol provides UART-based communication for controlling the ODEM3

**Protocol Architecture**: The protocol is organized with logical command separation:

## General Command Format
| Offset | Size (bytes) | Field                 | Description                                                                 |
|--------|--------------|-----------------------|-----------------------------------------------------------------------------|
| 0      | 2            | Start message flage   | `0x5555'                                                                    |
| 2      | 2            | Total message length  | Total message length including flage                                            |
| 4      | 2            | Command ID            |                                                                             |
| 6      | Variable     | Command payload       | The command-specific data.                                                  |


## General Response Format

All responses from the device, whether indicating success or an error, begin with a unified header structure designed for 32-bit alignment.

**1. Response Structure**

| Offset | Size (bytes) | Field                 | Description                                                                 |
|--------|--------------|-----------------------|-----------------------------------------------------------------------------|
| 0      | 2            | Start message flage   | `0x5555'                                                                    |
| 2      | 2            | Total message length  | Total message length after flage                                            |
| 4      | 2            | Original Command ID   | The command ID from the original request.                                   |
| 6      | 1            | Response Type         | 0 - Indicates Success; 1 - Error                                            |
| 7+     | Variable     | Actual Data Payload   | The command-specific success/error data                                     |


**2. Unsolicite Response Structure**

Unsolicited responses use the same response structure with Original Command ID replaced by special response ID values to indicate the type of unsolicited message: 


---

## Protocol Command List

- **Command 0:** Ping/Echo (Connection Test)
- **Command 1:** Read termeratures
- **Command 2:** AWG Vector Transfer
- **Command 3:** Config AWG
- **Command 4:** Set paraemter value
- **Command 5:** Run/Stop AWG

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

### Command 0: Ping/Echo (Connection Test)
- **Purpose:** Simple connectivity test to verify the TCP connection is alive.
- **Format:**
  - Command ID: 0
  - No paylad
- **Response payload:**
  - There is no data payload following the header for a Ping response.

### Command 1: Read termeratures
- **Purpose:** Simple connectivity test to verify the TCP connection is alive.
- **Format:**
  - Command ID: 1
  - No paylad
- **Response payload:**
  - Bytes 6-7: Temp A
  - Bytes 8-9: Temp B
  - Bytes 10-11: Temp C
  - Bytes 12-13: Temp D

  ### Command 2: AWG Vector Transfer - using fix size packets of 128 bytes of data payload (64 uint16_t)
- **Purpose:** Load AWG vector to device memory.
- **Format:**
  - Command ID: 2
  - **payload**:
    - Bytes 6-7: Packet number (for multi-packet transfers, big-endian)
    - Bytes 8+: Packet data (packet size × 2 bytes, littel-endian)
- **Response payload:**
  - Bytes 6-7: Packet number (echoed back for confirmation)

  ### Command 3: Config AWG command
- **Purpose:** Configure the AWG.
- **Format:**
  - Command ID: 3
  - **payload**:
    - Bytes 6-7: AWG vector length (number of uint16_t samples, big-endian)
- **Response payload:**
  - Response with no payload, indicating success or error in configuring the AWG with the previously transferred vector data.

  ### Command 4: Set paraemter value
- **Purpose:** Set parameter value.
- **Format:**
  - Command ID: 4
  - **payload**:
    - Bytes 6: parameter ID
    - Bytes 7-10: Parameter ID (big-endian)
- **Response payload:**
  - Response with no payload, indicating success or error in configuring the AWG with the previously transferred vector data.

### Command 5: Run/Stop AWG
- **Purpose:** Simple connectivity test to verify the TCP connection is alive.
- **Format:**
  - Command ID: 0
  - Bytes 6: Run/Stop flag (0 for Stop, 1 for Run)
- **Response payload:**
  - There is no data payload following the header for a Ping response.

