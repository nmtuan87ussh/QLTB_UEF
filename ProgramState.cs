using System;
using System.Drawing.Imaging;
using System.IO;

namespace QLTB_UEF
{
    public class ProgramState
    {
        public ErrorCorrection EncodeErrorCorrection = ErrorCorrection.M;
        public int EncodeModuleSize = 4;
        public int EncodeQuietZone = 16;
        public ImageFormat EncodeImageFormat = ImageFormat.Png;
        public string EncodeData = "QR Code encoder and decoder C# open source library including test/demo programs.";

        public int SaveSpecialImageWidth = 1920;
        public int SaveSpecialImageHeight = 1920;
        public int SaveSpecialImageMargin = 96;
        public double SaveSpecialCameraDistance = 3840.0;
        public double SaveSpecialCameraRotation = 0.0;
        public double SaveSpecialCameraViewAngle = 0.0;
        public int SaveSpecialFileFormat = 0;
        public double SaveSpecialPercentError = 0;
        public int SaveSpecialErrorArea = 0;

        public static ProgramState State;
        private static string FileName = "QRCodeEncodeState.txt";

        ////////////////////////////////////////////////////////////////////
        // Save Program State
        ////////////////////////////////////////////////////////////////////

        public static void SaveState()
        {
            // save state
            using (StreamWriter Output = new StreamWriter(new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                Output.WriteLine(string.Format("{0},{1},{2},{3}",
                    State.EncodeErrorCorrection.ToString(), State.EncodeModuleSize, State.EncodeQuietZone, State.EncodeImageFormat.ToString()));
                Output.WriteLine(State.EncodeData);
            }

            // exit
            return;
        }

        ////////////////////////////////////////////////////////////////////
        // Load Program State
        ////////////////////////////////////////////////////////////////////

        public static void LoadState()
        {
            State = new ProgramState();

            // load program state
            try
            {
                using (StreamReader Input = new StreamReader(new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    string Line = Input.ReadLine();
                    if (Line == null) throw new ApplicationException("Load state");
                    string[] Flds = Line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Flds.Length != 4) throw new ApplicationException("Load state");
                    switch (Flds[0][0])
                    {
                        case 'L':
                            State.EncodeErrorCorrection = ErrorCorrection.L;
                            break;

                        case 'M':
                            State.EncodeErrorCorrection = ErrorCorrection.M;
                            break;

                        case 'Q':
                            State.EncodeErrorCorrection = ErrorCorrection.Q;
                            break;

                        case 'H':
                            State.EncodeErrorCorrection = ErrorCorrection.H;
                            break;

                        default:
                            throw new ApplicationException("Load state");
                    }
                    State.EncodeModuleSize = int.Parse(Flds[1]);
                    State.EncodeQuietZone = int.Parse(Flds[2]);
                    switch (Flds[3])
                    {
                        case "Png":
                            State.EncodeImageFormat = ImageFormat.Png;
                            break;

                        case "Jpeg":
                            State.EncodeImageFormat = ImageFormat.Jpeg;
                            break;

                        case "Bmp":
                            State.EncodeImageFormat = ImageFormat.Bmp;
                            break;

                        case "Gif":
                            State.EncodeImageFormat = ImageFormat.Gif;
                            break;

                        default:
                            throw new ApplicationException("Load state");
                    }

                    Line = Input.ReadToEnd();
                    if (Line == null) throw new ApplicationException("Load state");
                    State.EncodeData = Line;
                }
            }
            catch
            {
                State = null;
            }

            // we have no program state file
            if (State == null)
            {
                // create new default program state
                State = new ProgramState();

                // save default
                SaveState();
            }

            // exit
            return;
        }

        ////////////////////////////////////////////////////////////////////
        // Load Program State
        ////////////////////////////////////////////////////////////////////

        public static void SetDefaultState()
        {
            // create new default program state
            State = new ProgramState();

            // save default
            SaveState();

            // exit
            return;
        }
    }
}
