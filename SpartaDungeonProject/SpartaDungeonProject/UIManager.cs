using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public struct COORD
    {
        int X { get; set; }
        int Y { get; set; }
    }

    public class UIManager
    {
        #region Member Variables
        /* Ui Box values */
        public int boxWidth;
        public int boxHeight;
        public int boxPosX;
        public int boxPosY;

        private bool _isBoxCreated = false;
        #endregion

        #region Main Methods
        public void UIBoxValueSetting()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            boxWidth = width / 2;
            boxHeight = height / 4;
            boxPosX = (width - boxWidth) / 2;
            boxPosY = (((height - boxHeight) / 4) * 4) - 2;
        }

        public void CreateUIMessageBox()
        {
            if(!_isBoxCreated)
            {
                UIBoxValueSetting();
                _isBoxCreated = true;
            }

            Console.SetCursorPosition(boxPosX, boxPosY - 1);
            PrintColorString("Message Box", ConsoleColor.Green);

            // 상단 테두리 그리기
            Console.SetCursorPosition(boxPosX, boxPosY);
            string boxRowLine = "┌" + new string('─', boxWidth - 2) + "┐";
            PrintColorString(boxRowLine, ConsoleColor.Green);

            for (int i = 1; i < boxHeight - 1; ++i)
            {
                Console.SetCursorPosition(boxPosX, boxPosY + i);
                string boxColSymbol = "│";
                string boxColLine = boxColSymbol + new string(' ', boxWidth - 2) + boxColSymbol;
                PrintColorString(boxColLine, ConsoleColor.Green);
            }

            // 하단 테두리 그리기
            Console.SetCursorPosition(boxPosX, boxPosY + boxHeight - 1);
            boxRowLine = "└" + new string('─', boxWidth - 2) + "┘";
            PrintColorString(boxRowLine, ConsoleColor.Green);
        }

        public void ClearUIMessageBox()
        {
            int colIdx = boxPosY + 1;
            int rowIdx = boxPosX + 1;

            for(int i = colIdx; i < (colIdx + boxHeight) - 2; ++i)
            {
                Console.SetCursorPosition(rowIdx, i);
                Console.Write(new String(' ', boxWidth - 2));
            }
        }

        // 특정 행을 지우는 함수
        public void ClearWidth(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.Write(new String(' ', Console.WindowWidth));
        }

        // 특정 행을 지우는 함수 어디서 ~ 어디까지
        public void ClearWidthToWidth(int whereRow, int fromRow)
        {
            for(int i = whereRow; i <= fromRow; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new String(' ', Console.WindowWidth));
            }
        }

        // 중앙 정렬하는 함수 기본 Height값은 0입니다.

        public void PrintCenterAlignString(string message, int top = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            int consoleWidth = Console.WindowWidth;
            int messageLength = GetMessageLength(message);

            // 중앙 정렬 위치 계산
            int padding = (consoleWidth - messageLength) / 2;

            // 한글, 영어 모두 고려한 중앙 정렬 출력.
            Console.SetCursorPosition(padding, top);
            PrintColorString(message, color);
        }

        // Width, Height가 중앙 정렬인 함수
        public void PrintCenterAlignWHString(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;
            int messageLength = GetMessageLength(message);

            // 중앙 정렬 위치 계산
            int wPadding = (consoleWidth - messageLength) / 2;
            int hPadding = (consoleHeight / 2);

            Console.SetCursorPosition(wPadding, hPadding);
            PrintColorString(message, color);
        }

        // 좌측, 우측 정렬
        public void PrintAlignSetterString(string message, bool isLeft = true, int div = 6, int top = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            int consoleWidth = Console.WindowWidth;
            int messageLength = GetMessageLength(message);
            int _div1Width = consoleWidth / div;

            if (isLeft)
            {
                Console.SetCursorPosition(_div1Width, top);
                PrintColorString(message, color);
            }
            else  // isRight
            {
                int padding = (consoleWidth - messageLength) - _div1Width;
                Console.SetCursorPosition(padding, top);
                PrintColorString(message, color);
            }

        }

        public void PrintUIBoxMessage(string message, int top = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            int uiPosX = boxPosX + 1;
            int uiPosY = boxPosY + 1 + top;

            int maxBoxLength = boxWidth - 2;
            string[] lines = WordWrap(message, maxBoxLength);

            for(int i = 0; i < lines.Length; ++i)
            {
                Console.SetCursorPosition(uiPosX, uiPosY + i);
                PrintColorString(lines[i], color);
            }
        }

        public void ShowTableList(List<string> columns, List<Dictionary<string, string>> data, int top, ConsoleColor color = ConsoleColor.Gray)
        {
            int consoleWidth = Console.WindowWidth;
            int numRows = data.Count;
            int consoleDiv = consoleWidth / 6;

            // 각 컬럼의 너비를 설정합니다.
            int itemNameWidth = 20; // 아이템 이름 너비
            int itemTypeStatWidth = 25; // 아이템 타입과 스탯 합친 너비
            int itemQuantityWidth = 10; // 수량 너비
            int descriptionWidth = consoleWidth - consoleDiv * 2 - itemNameWidth - itemTypeStatWidth - itemQuantityWidth - 3; // 나머지 너비

            // 테이블 헤더 출력
            string headerRow = "|";
            headerRow += AlignText("아이템 이름", itemNameWidth - 1, false) + "|";
            headerRow += AlignText("아이템 타입 / 스탯", itemTypeStatWidth - 1, false) + "|";
            headerRow += AlignText("수량", itemQuantityWidth - 1, false) + "|";
            headerRow += AlignText("설명", descriptionWidth - 1, false) + "|";

            // 테이블 상단 테두리 출력
            Console.SetCursorPosition(consoleDiv, top);
            PrintColorString(new string('─', consoleWidth - consoleDiv * 2 - 2), ConsoleColor.Red);

            // 테이블 헤더 출력
            Console.SetCursorPosition(consoleDiv, top + 1);
            PrintColorString(headerRow, ConsoleColor.Red);

            // 테이블 중간 테두리 출력
            Console.SetCursorPosition(consoleDiv, top + 2);
            PrintColorString(new string('─', consoleWidth - consoleDiv * 2 - 2), ConsoleColor.Red);

            // 테이블 데이터 출력
            for (int i = 0; i < numRows; i++)
            {
                string dataRow = "|";
                string itemName = data[i]["ItemName"];
                string itemType = data[i]["ItemType"];
                string itemStat = data[i]["ItemStat"];
                string itemQuantity = data[i]["ItemQuantities"];
                string description = data[i]["ItemDescription"];

                if(+)

                if (itemType == "WEAPON")
                {
                    string message = "공격력 + " + itemStat;
                    itemType = message;
                }
                else if (itemType == "ARMOR")
                {
                    string message = "방어력 + " + itemStat;
                    itemType = message;
                }

                dataRow += AlignText(itemName, itemNameWidth - 1, false) + "|";
                dataRow += AlignText($"{itemType}", itemTypeStatWidth - 1, false) + "|";
                dataRow += AlignText(itemQuantity, itemQuantityWidth - 1, false) + "|";
                dataRow += AlignText(description, descriptionWidth - 1, false) + "|";

                Console.SetCursorPosition(consoleDiv, top + 3 + i);
                PrintColorString(dataRow, color);
            }

            // 테이블 하단 테두리 출력
            Console.SetCursorPosition(consoleDiv, top + numRows + 3);
            PrintColorString(new string('─', consoleWidth - consoleDiv * 2 - 2), color);
        }
        #endregion

        #region Helper Methods
        private string[] WordWrap(string message, int maxLineLength)
        {
            List<string> lines = new List<string>();
            string[] words = message.Split(' ');

            string currentLines = string.Empty;

            foreach(var word in words)
            {
                if (GetMessageLength(currentLines + word) <= maxLineLength)
                    currentLines += word + " ";
                else
                {
                    lines.Add(currentLines.Trim());
                    currentLines = word + " ";
                }
            }

            if(!string.IsNullOrEmpty(currentLines))
                lines.Add(currentLines.Trim());

            return lines.ToArray();
        }

        public static void PrintColorString(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        // 문자열 길이를 반환 (한글 2byte, 영어 1byte)
        public int GetMessageLength(string message)
        {
            int length = 0;

            foreach(char ch in message)
                // 유니코드 한글의 범위
                length += (ch >= '\uAC00' && ch <= '\uD7A3') ? 2 : 1;

            return length;
        }

        public int GetMessageIndex(string message, int width)
        {
            int length = 0;
            int index = 0;

            foreach(char ch in message)
            {
                length += (ch >= '\uAC00' && ch <= '\uD7A3') ? 2 : 1;
                if (length <= width)
                    ++index;
                else
                    break;
            }

            return index;
        }

        private string AlignText(string text, int width, bool isLeftAligned)
        {
            int textLength = GetMessageLength(text);
            if (textLength >= width)
            {
                return text.Substring(0, GetMessageIndex(text, width));
            }
            else
            {
                if (!isLeftAligned)
                {
                    int paddingLength = width - textLength;
                    int leftPadding = paddingLength / 2;
                    int rightPadding = paddingLength - leftPadding;
                    return new string(' ', leftPadding) + text + new string(' ', rightPadding);
                }
                else
                    return text.PadLeft(width - (width - GetMessageIndex(text, textLength)));
            }
        }
        #endregion
    }
}
