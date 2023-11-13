
namespace SpartaDungeon
{
    public class Manager_UI
    {
        #region Member Variables

        public int consoleWidth;
        public int consoleHeight;

        /* UI Message Box Values */
        public int boxWidth;
        public int boxHeight;
        public int boxPosX;
        public int boxPosY;

        private bool _isBoxCreated = false;     // UI Message Box를 단 한번도 만든적이 없다면

        #endregion

        #region Constructor & Initalize
        public Manager_UI()
        {
            this.Initialize();
        }
        private void Initialize()
        {
            consoleWidth = Console.WindowWidth;
            consoleHeight = Console.WindowHeight;

            if (!_isBoxCreated)
                this.UIMessageBoxValueInit();
        }
        private void UIMessageBoxValueInit()
        {
            boxWidth = consoleWidth / 2;
            boxHeight = consoleHeight / 4;
            boxPosX = (consoleWidth - boxWidth) / 2;
            boxPosY = ((consoleHeight - boxHeight) / 4 * 4) - 2;

            _isBoxCreated = true;
        }
        #endregion

        #region Draw Methods (그리는 메소드)
        // Main 게임에 사용될 메시지 박스를 그리는 메소드
        public void DrawUIMessageBox()
        {
            SetColor(ConsoleColor.Green);
            SetPos(boxPosX, boxPosY - 1);
            PrintText("Message  Box");

            // Left-Top Border
            string boxCornerRowLine = "┌" + new string('─', boxWidth - 2) + "┐";

            SetPos(boxPosX, boxPosY);
            PrintText(boxCornerRowLine);

            // Centers Border
            for(int row = 1; row < boxHeight - 1; ++row)
            {
                SetPos(boxPosX, boxPosY + row);
                string boxRowLine = "│" + new string(' ', boxWidth - 2) + "│";
                PrintText(boxRowLine);
            }

            // Right-Bottom Border
            boxCornerRowLine = "└" + new string('─', boxWidth - 2) + "┘";

            SetPos(boxPosX, boxPosY + boxHeight - 1);
            PrintText(boxCornerRowLine);
            ResetColor();
        }
        #endregion

        #region Print Methods (텍스트 정렬 및 출력 메소드)
        // 중앙 정렬하는 메소드
        public void PrintTextAlignCenter(string message, int topCursor = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            int padding = (consoleWidth - GetMessageUTFLength(message)) / 2;

            SetPos(padding, topCursor);
            PrintTextToColor(message, color);
        }

        // 화면 정중앙에 정렬하는 메소드
        public void PrintTextAlignCenterToCenter(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            int wPadding = (consoleWidth - GetMessageUTFLength(message)) / 2;
            int hPadding = (consoleHeight / 2);

            SetPos(wPadding, hPadding);
            PrintTextToColor(message, color);
        }

        // 좌측, 우측 정렬을 division 상수값으로 하는 메소드
        public void PrintTextAlignLeftRight(string message, bool isLeft = true, 
            int division = 6, int topCursor = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            int messageLength = GetMessageUTFLength(message);
            int divisionWidth = consoleWidth / division;

            if(isLeft)
            {
                SetPos(divisionWidth, topCursor);
                PrintTextToColor(message, color);
            }
            else
            {
                int padding = (consoleWidth - messageLength) - divisionWidth;
                SetPos(padding, topCursor);
                PrintTextToColor(message, color);
            }
        }

        // Message Box에 바로 출력하는 함수
        public void PrintTextBoxMessage(string message, int topCursor = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            int posX = boxPosX + 1;
            int posY = boxPosY + 1 + topCursor;

            int boxLength = boxWidth - 2;
            string[] messages = WordWrap(message, boxLength);

            for(int idx = 0; idx < messages.Length; ++idx)
            {
                SetPos(posX, posY + idx);
                PrintTextToColor(messages[idx], color);
            }
        }
        #endregion

        #region Clear Methods (지우는 메소드)
        // 메시지 박스 내용을 클리어하는 메소드
        public void ClearUIMessageBox()
        {
            int col = boxPosY + 1;
            int row = boxPosX + 1;

            for(int idx = col; idx < (col + boxHeight) - 2; ++idx)
            {
                SetPos(row, idx);
                PrintText(new string(' ', boxWidth));
            }
        }

        // 특정 행을 지우는 메소드
        public void ClearRow(int row)
        {
            SetPos(0, row);
            PrintText(new string(' ', consoleWidth));
        }

        // 특정 행 ~ 특정 행까지
        public void ClearRowToRow(int startRow, int endRow)
        {
            for(int row = startRow; row <= endRow; ++row)
            {
                SetPos(0, row);
                PrintText(new string(' ', consoleWidth));
            }
        }
        #endregion

        #region Helper Methods
        // 유니코드를 포함하여 특정 길이를 넘어가면 반대줄로 개행 하는 메소드
        private string[] WordWrap(string message, int maxLineLength)
        {
            List<string> messageList = new List<string>();
            string[] words = message.Split(' ');

            string currentMessage = string.Empty;

            foreach (var word in words)
            {
                if (GetMessageUTFLength(currentMessage + word) <= maxLineLength)
                    currentMessage += word + " ";
                else
                {
                    messageList.Add(currentMessage.Trim());
                    currentMessage = word + " ";
                }
            }

            if (!string.IsNullOrEmpty(currentMessage))
                messageList.Add(currentMessage.Trim());

            return messageList.ToArray();
        }

        // 좌우 공백 채워 정렬하는 메소드
        private string GetPaddingToMessage(string message, int width, bool isLeftAligned = false)
        {
            int messageLength = GetMessageUTFLength(message);

            if (messageLength >= width)
                return message.Substring(0, GetMessageUTFIndex(message, width));
            else
            {
                if (!isLeftAligned)
                {
                    int paddingLength = width - messageLength;
                    int leftPadding = paddingLength / 2;
                    int rightPadding = paddingLength - leftPadding;

                    return new string(' ', leftPadding) + message + new string(' ', rightPadding);
                }
                else
                    return message.PadLeft(width - (GetMessageUTFIndex(message, messageLength) - width));
            }
        }
        // 한글과 같은 2byte를 길이를 반환하기 위한 메소드
        public int GetMessageUTFLength(string message)
        {
            int textLength = 0;

            foreach(char ch in message)
                textLength += (ch >= '\uAC00' && ch <= '\uD7A3') ? 2 : 1;

            return textLength;
        }
        // 한글 문자열이 길이를 넘어가면 해당 인덱스를 반환하는 메소드
        public int GetMessageUTFIndex(string message, int width)
        {
            int textLength = 0;
            int textIndex = 0;

            foreach(char ch in message)
            {
                textLength += (ch >= '\uAC00' && ch <= '\uD7A3') ? 2 : 1;
                if (textLength <= width)
                    ++textIndex;
                else
                    break;
            }

            return textIndex;
        }
        // 문자열에 색깔을 더해주는 함수
        public static void PrintTextToColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        #region Rename Function
        public static void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        public static void ResetColor()
        {
            Console.ResetColor();
        }
        public static void SetPos(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
        }
        public static void PrintText(string message)
        {
            Console.Write(message);
        }
        #endregion
        #endregion
    }
}