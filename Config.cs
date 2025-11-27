using System;
using System.Collections.Generic;
using System.Text;

namespace OdemControl
{
    partial class Form1
    {
        // Sensitivity --> Normal: 0x81010E3C; High: 0x80010F3c	
        List<uint> sensitivity = new List<uint>() { 0x81010E3C, 0x80010F3c };
        private void cofigdevice()
        {
            string Error = "";
            while (confState != (int)confStates.DONE)
            {
                switch (confState)
                {
                    case (int)confStates.IDLE:
                        confState++;
                        break;

                    case (int)confStates.SEND_CAPTURE_DELAY:
                        LogMessage("Configuring: Capture_Delay");
                        Error = WriteRegWaitResp(0xFF200024, new List<uint> { deviceParameters["Capture_Delay"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Capture Delay:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.RESET_DSP:
                        LogMessage("Configuring: Reset DSP");
                        Error = WriteRegWaitResp(0xFF200010, new List<uint> { 0x4100004 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Reset DSP:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_SENSITIVITY:
                        LogMessage("Configuring: Sensitivity");
                        Error = WriteRegWaitResp(0xFF200010, new List<uint> { sensitivity[appSetting.sensitivity] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Senitivity:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_RANGE:  // CFAR multiplication
                        LogMessage("Configuring: CFAR multiplication");
                        Error = WriteRegWaitResp(0xFF20007C, new List<uint> { 0x00000808 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Range:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_RETRO_LEVEL:
                        LogMessage("Configuring: Retro level");
                        Error = WriteRegWaitResp(0xFF200070, new List<uint> { 10000 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Relro:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_CHIRP_WAVEFORM:
                        confState = (int)confStates.SET_CHIRP_GAIN;
                        break;

                    case (int)confStates.SET_CHIRP_GAIN:
                        LogMessage("Configuring: SET_CHIRP_GAIN");
                        Error = WriteI2CWaitResp(3, 0x4B, 0x14, 0x1C, new List<uint> { deviceParameters["Chirp AWG gain"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;
                     case (int)confStates.SET_PM_CONTROL:
                        LogMessage("Configuring: SET_PM1_CONTROL");
                        Error = WriteI2CWaitResp(3, 0x4A, 0x14, 0x1C, new List<uint> { 0 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        LogMessage("Configuring: SET_PM2_CONTROL");
                        Error = WriteI2CWaitResp(3, 0x48, 0x14, 0x1C, new List<uint> { 1 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.LOAD_SSH_DRIVER:
                        confState++;
                        break;

                    case (int)confStates.SET_LO:
                        LogMessage("Configuring: SET_LO");
                        Error = WriteI2CWaitResp(7, 0x4B, 0x14, 0x1C, new List<uint> { deviceParameters["LO"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX_SOA1:
                        LogMessage("Configuring: SET_TX_SOA1");
                        Error = WriteI2CWaitResp(7, 0x4A, 0x14, 0x19, new List<uint> { deviceParameters["TxSOA1"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX_SOA2:
                        LogMessage("Configuring: SET_TX_SOA2");
                        Error = WriteI2CWaitResp(7, 0x4A, 0x14, 0x1C, new List<uint> { deviceParameters["TxSOA2"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_0_9:
                        LogMessage("Configuring: SET_TX3_0_9");
                        Error = WriteI2CWaitResp(7, 0x49, 0x14, 0x19, new List<uint> { deviceParameters["Tx3_0_9"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_10_19:
                        LogMessage("Configuring: SET_TX3_10_19");
                        Error = WriteI2CWaitResp(7, 0x49, 0x14, 0x1C, new List<uint> { deviceParameters["Tx3_10_19"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_20_29:
                        LogMessage("Configuring: SET_TX3_20_29");
                        Error = WriteI2CWaitResp(7, 0x48, 0x14, 0x19, new List<uint> { deviceParameters["Tx3_20_29"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_30_39:
                        LogMessage("Configuring: SET_TX3_30_39");
                        Error = WriteI2CWaitResp(7, 0x48, 0x14, 0x1C, new List<uint> { deviceParameters["Tx3_30_39"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_1:
                        LogMessage("Configuring: badGoodIndxs_High");
                        Error = WriteRegWaitResp(0xFF200028, confFiles["badGoodIndxs_High"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_2:
                        LogMessage("Configuring: badGoodIndxs_Low");
                        Error = WriteRegWaitResp(0xFF20002C, confFiles["badGoodIndxs_Low"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_Low:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_3:
                        LogMessage("Configuring: 128Bins_Final to 0xFF248000");
                        Error = WriteRegWaitResp(0xFF248000, confFiles["128Bins_Final"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 128Bins_Final:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_4:
                        LogMessage("Configuring: 128Bins_Final to 0xFF248080");
                        Error = WriteRegWaitResp(0xFF248080, confFiles["128Bins_Final"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 128Bins_Final:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_5:
                        LogMessage("Configuring: blackmanHarris_DEC");
                        Error = WriteRegWaitResp(0xFF340000, confFiles["blackmanHarris_DEC"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending blackmanHarris_DEC:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_6:
                        LogMessage("Configuring: 2kWin");
                        Error = WriteRegWaitResp(0xFF346000, confFiles["2kWin"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 2kWin:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_OPTOTUNE_X:
                        confState++;
                        break;

                    case (int)confStates.SET_OPTOTUNE_Y:
                        confState++;
                        break;

                    case (int)confStates.SET_MIRROR_FREQUENCY:
                        LogMessage("Configuring: SET_MIRROR_FREQUENCY");
                        uint iFreq = BitConverter.SingleToUInt32Bits((float)scanModes[modes[appSetting.scanModeNum]].mirror);
                        Error = SPIWriteRegWaitResp(0x68, 0x04, iFreq);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_NUMBER_OF_POINTS:
                        confState++;
                        break;

                    case (int)confStates.RUN_OPTOTUNE_CALIBRATION:
                        confState++;
                        break;

                    case (int)confStates.DONE:
                        break;

                    default:
                        MessageBox.Show("Configuration is already in progress.", "Configuration In Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

    }
}
