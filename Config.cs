using System;
using System.Collections.Generic;
using System.Text;

namespace OdemControl
{
    partial class Form1
    {
        private void cofigdevice()
        {
            while (confState != (int)confStates.DONE)
            {
                switch (confState)
                {
                    case (int)confStates.IDLE:
                        confState = (int)confStates.SEND_CAPTURE_DELAY;
                        break;

                    case (int)confStates.SEND_CAPTURE_DELAY:
                        confState = (int)confStates.RESET_DSP;
                        break;

                    case (int)confStates.RESET_DSP:
                        confState = (int)confStates.SET_SENSITIVITY;
                        break;

                    case (int)confStates.SET_SENSITIVITY:
                        confState = (int)confStates.SET_RANGE;
                        break;

                    case (int)confStates.SET_RANGE:
                        confState = (int)confStates.SET_RETRO_LEVEL;
                        break;

                    case (int)confStates.SET_RETRO_LEVEL:
                        confState = (int)confStates.SET_CHIRP_WAVEFORM;
                        break;

                    case (int)confStates.SET_CHIRP_WAVEFORM:
                        confState = (int)confStates.SET_CHIRP_GAIN;
                        break;

                    case (int)confStates.SET_CHIRP_GAIN:
                        confState = (int)confStates.SET_PM_CONTROL;
                        break;

                    case (int)confStates.SET_PM_CONTROL:
                        confState = (int)confStates.SET_DRIVER_BOARD;
                        break;

                    case (int)confStates.SET_DRIVER_BOARD:
                        confState = (int)confStates.SET_LO;
                        break;

                    case (int)confStates.SET_LO:
                        confState = (int)confStates.SET_TX_SOA1;
                        break;

                    case (int)confStates.SET_TX_SOA1:
                        confState = (int)confStates.SET_TX_SOA2;
                        break;

                    case (int)confStates.SET_TX_SOA2:
                        confState = (int)confStates.SET_TX3_0_9;
                        break;

                    case (int)confStates.SET_TX3_0_9:
                        confState = (int)confStates.SET_TX3_10_19;
                        break;

                    case (int)confStates.SET_TX3_10_19:
                        confState = (int)confStates.SET_TX3_20_29;
                        break;

                    case (int)confStates.SET_TX3_20_29:
                        confState = (int)confStates.SET_TX3_30_39;
                        break;

                    case (int)confStates.SET_TX3_30_39:
                        confState = (int)confStates.SET_VECTOR_1;
                        break;

                    case (int)confStates.SET_VECTOR_1:
                        confState = (int)confStates.SET_VECTOR_2;
                        break;

                    case (int)confStates.SET_VECTOR_2:
                        confState = (int)confStates.SET_VECTOR_3;
                        break;

                    case (int)confStates.SET_VECTOR_3:
                        confState = (int)confStates.SET_VECTOR_4;
                        break;

                    case (int)confStates.SET_VECTOR_4:
                        confState = (int)confStates.SET_VECTOR_5;
                        break;

                    case (int)confStates.SET_VECTOR_5:
                        confState = (int)confStates.SET_VECTOR_6;
                        break;

                    case (int)confStates.SET_VECTOR_6:
                        confState = (int)confStates.SET_OPTOTUNE_X;
                        break;

                    case (int)confStates.SET_OPTOTUNE_X:
                        confState = (int)confStates.SET_OPTOTUNE_Y;
                        break;

                    case (int)confStates.SET_OPTOTUNE_Y:
                        confState = (int)confStates.SET_MIRROR_FREQUENCY;
                        break;

                    case (int)confStates.SET_MIRROR_FREQUENCY:
                        confState = (int)confStates.SET_NUMBER_OF_POINTS;
                        break;

                    case (int)confStates.SET_NUMBER_OF_POINTS:
                        confState = (int)confStates.RUN_OPTOTUNE_CALIBRATION;
                        break;

                    case (int)confStates.RUN_OPTOTUNE_CALIBRATION:
                        confState = (int)confStates.DONE;
                        break;

                    default:
                        MessageBox.Show("Configuration is already in progress.", "Configuration In Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }
        private byte[] WriteReg(uint add, List<uint> vals)
        {
            List<byte> data = new List<byte>();
            data.Add(0x01);         // Write command
            data.Add(0x00);         // Reserved
            data.AddRange(GetBytesBigEndian(add));
            data.AddRange(GetBytesBigEndian((uint)vals.Count));
            foreach (uint v in vals)
                data.AddRange(GetBytesBigEndian(v));
            return data.ToArray();
        }


    }
}
