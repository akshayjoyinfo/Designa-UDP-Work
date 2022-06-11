using System;
using System.Collections.Generic;
using System.Text;

namespace LedDisplayTester
{
    public class Errer
    {
        #region
        public static string GetError(int err)
        {
            string errer = "";
            switch (err)
            {
                case bxyq_err.ERR_HTTP_REQUEST_EMPTY_HTTP:
                    errer = "ERR_HTTP_REQUEST_EMPTY_HTTP";
                    break;
                case bxyq_err.ERR_HTTP_REQUEST_METHOD_HTTP:
                    errer = "ERR_HTTP_REQUEST_METHOD_HTTP";
                    break;
                case bxyq_err.ERR_PROTOCOL_PARSE:
                    errer = "ERR_PROTOCOL_PARSE";
                    break;
                case bxyq_err.ERR_PROTOCOL_NAME:
                    errer = "ERR_PROTOCOL_NAME";
                    break;
                case bxyq_err.ERR_PROTOCOL_VERSION:
                    errer = "ERR_PROTOCOL_VERSION";
                    break;
                case bxyq_err.ERR_PID_PID:
                    errer = "ERR_PID_PID";
                    break;
                case bxyq_err.ERR_BARCODE:
                    errer = "ERR_BARCODE";
                    break;
                case bxyq_err.ERR_HTTP_REQUEST_PARAMETER_KEY:
                    errer = "ERR_HTTP_REQUEST_PARAMETER_KEY";
                    break;
                case bxyq_err.ERR_CONFIG_PARSE:
                    errer = "ERR_CONFIG_PARSE";
                    break;
                case bxyq_err.ERR_PERMISSION:
                    errer = "ERR_PERMISSION";
                    break;
                case bxyq_err.ERR_INVALID_AUTHENTICATION:
                    errer = "ERR_INVALID_AUTHENTICATION";
                    break;
                case bxyq_err.ERR_ACCESS_VIOLATION:
                    errer = "ERR_ACCESS_VIOLATION";
                    break;
                case bxyq_err.ERR_IO_READ_WRITE:
                    errer = "ERR_IO_READ_WRITE";
                    break;
                case bxyq_err.ERR_COMMAND_PARAMETER_KEY:
                    errer = "ERR_COMMAND_PARAMETER_KEY";
                    break;
                case bxyq_err.ERR_COMMAND_CALL:
                    errer = "ERR_COMMAND_CALL";
                    break;
                case bxyq_err.ERR_COMMAND_PROCESS:
                    errer = "ERR_COMMAND_PROCESS";
                    break;
                case bxyq_err.ERR_COMMAND_NOT_EXISTS:
                    errer = "ERR_COMMAND_NOT_EXISTS";
                    break;
                case bxyq_err.ERR_COMMAND_PARAMETER_EMPTY:
                    errer = "ERR_COMMAND_PARAMETER_EMPTY";
                    break;
                case bxyq_err.ERR_COMMAND_EXECUTE:
                    errer = "ERR_COMMAND_EXECUTE";
                    break;
                case bxyq_err.ERR_COMMAND_PARAMETER_VALUE:
                    errer = "ERR_COMMAND_PARAMETER_VALUE";
                    break;
                case bxyq_err.ERR_USER_NOT_EXISTS:
                    errer = "ERR_USER_NOT_EXISTS";
                    break;
                case bxyq_err.ERR_USER_PASSWORD:
                    errer = "ERR_USER_PASSWORD";
                    break;
                case bxyq_err.ERR_STORAGE_MEDIA_NOT_EXISTS:
                    errer = "ERR_STORAGE_MEDIA_NOT_EXISTS";
                    break;
                case bxyq_err.ERR_FILE_PATH:
                    errer = "ERR_FILE_PATH";
                    break;
                case bxyq_err.ERR_MAC_FORMAT_MAC:
                    errer = "ERR_MAC_FORMAT_MAC";
                    break;
                case bxyq_err.ERR_UDP_TRANSMIT_UDP:
                    errer = "ERR_UDP_TRANSMIT_UDP";
                    break;
                case bxyq_err.ERR_VERIFICATION_CODE:
                    errer = "ERR_VERIFICATION_CODE";
                    break;
                case bxyq_err.ERR_NO_FIRMWARE:
                    errer = "ERR_NO_FIRMWARE";
                    break;
                case bxyq_err.ERR_USER_WORK_PATH:
                    errer = "ERR_USER_WORK_PATH";
                    break;
                case bxyq_err.ERR_PLAYER_CMD:
                    errer = "ERR_PLAYER_CMD";
                    break;
                case bxyq_err.ERR_GET_WIFI_LIST:
                    errer = "ERR_GET_WIFI_LIST";
                    break;
                case bxyq_err.ERR_WIFI_CONNECT_TIMEOUT:
                    errer = "ERR_WIFI_CONNECT_TIMEOUT";
                    break;
                case bxyq_err.ERR_HOTSPOT_NOT_FOUND:
                    errer = "ERR_HOTSPOT_NOT_FOUND";
                    break;
                case bxyq_err.ERR_WIFI_PASSWORD:
                    errer = "ERR_WIFI_PASSWORD";
                    break;
                case bxyq_err.ERR_NETWORK_RESTART:
                    errer = "ERR_NETWORK_RESTART";
                    break;
                case bxyq_err.ERR_Unknow:
                    errer = "ERR_Unknow";
                    break;
                default:
                    errer = "Some Error";
                    break;
            }
            return errer;
        }
        public class bxyq_err
        {
            public const int ERR_Unknow = -1;
            public const int ERR_HTTP_REQUEST_EMPTY_HTTP = 0x01;
            public const int ERR_HTTP_REQUEST_METHOD_HTTP = 0x02;
            public const int ERR_PROTOCOL_PARSE = 0x03;
            public const int ERR_PROTOCOL_NAME = 0x04;
            public const int ERR_PROTOCOL_VERSION = 0x05;
            public const int ERR_PID_PID = 0x06;
            public const int ERR_BARCODE = 0x07;
            public const int ERR_HTTP_REQUEST_PARAMETER_KEY = 0x08;
            public const int ERR_CONFIG_PARSE = 0x09;
            public const int ERR_PERMISSION = 0x0a;
            public const int ERR_INVALID_AUTHENTICATION = 0x0b;
            public const int ERR_ACCESS_VIOLATION = 0x0c;
            public const int ERR_IO_READ_WRITE = 0x0d;
            public const int ERR_COMMAND_PARAMETER_KEY = 0x0e;
            public const int ERR_COMMAND_CALL = 0x0f;
            public const int ERR_COMMAND_PROCESS = 0x10;
            public const int ERR_COMMAND_NOT_EXISTS = 0x11;
            public const int ERR_COMMAND_PARAMETER_EMPTY = 0x12;
            public const int ERR_COMMAND_EXECUTE = 0x13;
            public const int ERR_COMMAND_PARAMETER_VALUE = 0x14;
            public const int ERR_USER_NOT_EXISTS = 0x15;
            public const int ERR_USER_PASSWORD = 0x16;
            public const int ERR_STORAGE_MEDIA_NOT_EXISTS = 0x17;
            public const int ERR_FILE_PATH = 0x18;
            public const int ERR_MAC_FORMAT_MAC = 0x19;
            public const int ERR_UDP_TRANSMIT_UDP = 0x1a;
            public const int ERR_VERIFICATION_CODE = 0x1b;
            public const int ERR_NO_FIRMWARE = 0x1c;
            public const int ERR_USER_WORK_PATH = 0x1d;
            public const int ERR_PLAYER_CMD = 0x1e;
            public const int ERR_GET_WIFI_LIST = 0x1f;
            public const int ERR_WIFI_CONNECT_TIMEOUT = 0x20;
            public const int ERR_HOTSPOT_NOT_FOUND = 0x21;
            public const int ERR_WIFI_PASSWORD = 0x22;
            public const int ERR_NETWORK_RESTART = 0x23;
        }
        #endregion
    }
}
