namespace Day3
{
    internal class SolutionV1
    {
        internal static void SolvePartOne(List<string> input)
        {
            var validNumbers = new List<int>();

            var sourceMap = ConvertTo2DArray(input);
            var rowsCount = sourceMap.GetLength(0);
            var columnsCount = sourceMap.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                bool isAdjacent = false;
                string? currentNumber = string.Empty;

                var tmpRowNumbers = new List<int>();

                for (var colIndex = 0; colIndex < columnsCount; colIndex++)
                {
                    var currentSymbol = sourceMap[rowIndex, colIndex];

                    if (char.IsDigit(currentSymbol))
                    {
                        currentNumber += currentSymbol;
                        if (!isAdjacent)
                        {
                            isAdjacent = IsAdjacentToSymbol(sourceMap, rowIndex, colIndex);
                        }

                        if (colIndex == columnsCount - 1 && isAdjacent)
                        {
                            var number = int.Parse(currentNumber);
                            validNumbers.Add(number);
                            tmpRowNumbers.Add(number);
                            currentNumber = string.Empty;
                            isAdjacent = false;
                        }
                    }
                    else
                    {
                        if (isAdjacent)
                        {
                            var number = int.Parse(currentNumber);
                            validNumbers.Add(number);
                            tmpRowNumbers.Add(number);
                            currentNumber = string.Empty;
                            isAdjacent = false;
                        }
                        currentNumber = string.Empty;
                    }
                }

                Console.WriteLine($"Row {rowIndex + 1}: {string.Join(",", tmpRowNumbers)}");
            }

            Console.WriteLine("Sum: " + validNumbers.Sum());
        }

        static bool IsAdjacentToSymbol(char[,] sourceMap, int rowIndex, int columnIndex)
        {
            return IsLeftAdjacent(sourceMap, rowIndex, columnIndex)
              || IsRightAdjacent(sourceMap, rowIndex, columnIndex)
              || IsTopAdjacent(sourceMap, rowIndex, columnIndex)
              || IsBottomAdjacent(sourceMap, rowIndex, columnIndex)
              || IsDiagonalAdjacent(sourceMap, rowIndex, columnIndex);
        }

        static bool IsLeftAdjacent(char[,] sourceMap, int rowIndex, int columnIndex)
        {
            if (columnIndex - 1 <= 0)
            {
                return false;
            }

            return IsSpecialSymbol(sourceMap[rowIndex, columnIndex - 1]);
        }

        static bool IsRightAdjacent(char[,] sourceMap, int rowIndex, int columnIndex)
        {
            if (columnIndex + 1 >= sourceMap.GetLength(1))
            {
                return false;
            }

            return IsSpecialSymbol(sourceMap[rowIndex, columnIndex + 1]);
        }

        static bool IsTopAdjacent(char[,] sourceMap, int rowIndex, int columnIndex)
        {
            if (rowIndex - 1 <= 0)
            {
                return false;
            }

            return IsSpecialSymbol(sourceMap[rowIndex - 1, columnIndex]);
        }

        static bool IsBottomAdjacent(char[,] sourceMap, int rowIndex, int columnIndex)
        {
            if (rowIndex + 1 >= sourceMap.GetLength(0))
            {
                return false;
            }

            return IsSpecialSymbol(sourceMap[rowIndex + 1, columnIndex]);
        }

        static bool IsDiagonalAdjacent(char[,] sourceMap, int rowIndex, int colIndex)
        {
            var rowsCount = sourceMap.GetLength(0);
            var columnsCount = sourceMap.GetLength(1);

            var isAdjacent = false;

            // LEFT UPPER 
            var leftUpperRowIndex = rowIndex - 1;
            var leftUpperColIndex = colIndex - 1;
            if (!NeighbourNodeIsOutOfBounds(leftUpperRowIndex, leftUpperColIndex, rowsCount, columnsCount)
              && leftUpperRowIndex >= 0
              && leftUpperColIndex >= 0)
            {
                var node = sourceMap[leftUpperRowIndex, leftUpperColIndex];
                isAdjacent |= IsSpecialSymbol(node);
            }

            // RIGHT UPPER 
            var rightUpperRowIndex = rowIndex - 1;
            var rightUpperColIndex = colIndex + 1;
            if (!NeighbourNodeIsOutOfBounds(rightUpperRowIndex, rightUpperColIndex, rowsCount, columnsCount)
              && rightUpperRowIndex >= 0
              && rightUpperColIndex <= sourceMap.GetLength(1))
            {
                var node = sourceMap[rightUpperRowIndex, rightUpperColIndex];
                isAdjacent |= IsSpecialSymbol(node);
            }

            // LEFT BOTTOM
            var leftBottomRowIndex = rowIndex + 1;
            var leftBottomColIndex = colIndex - 1;
            if (!NeighbourNodeIsOutOfBounds(leftBottomRowIndex, leftBottomColIndex, rowsCount, columnsCount)
              && leftBottomRowIndex <= sourceMap.GetLength(0)
              && leftBottomColIndex >= 0)
            {
                var node = sourceMap[leftBottomRowIndex, leftBottomColIndex];
                isAdjacent |= IsSpecialSymbol(node);
            }

            // RIGHT BOTTOM
            var rightBottomRowIndex = rowIndex + 1;
            var rightBottomColIndex = colIndex + 1;
            if (!NeighbourNodeIsOutOfBounds(rightBottomRowIndex, rightBottomColIndex, rowsCount, columnsCount)
              && rightBottomRowIndex <= sourceMap.GetLength(0)
              && rightBottomColIndex <= sourceMap.GetLength(1))
            {
                var node = sourceMap[rightBottomRowIndex, rightBottomColIndex];
                isAdjacent |= IsSpecialSymbol(node);
            }

            return isAdjacent;
        }

        static bool NeighbourNodeIsOutOfBounds(int rowIndex, int columnIndex, int rowsCount, int columnsCount)
        {
            return rowIndex <= 0 || columnIndex <= 0 || rowIndex >= rowsCount || columnIndex >= columnsCount;
        }

        static bool IsSpecialSymbol(char symbol)
        {
            return !char.IsDigit(symbol) && symbol != '.';
        }

        static char[,] ConvertTo2DArray(List<string> inputLines)
        {
            var rowsCount = inputLines.Count;
            var columnsCount = inputLines[0].Length;

            var array = new char[rowsCount, columnsCount];
            for (int rowIndex = 0; rowIndex < inputLines.Count; rowIndex++)
            {
                string? line = inputLines[rowIndex];
                for (int colIndex = 0; colIndex < line.Length; colIndex++)
                {
                    char @char = line[colIndex];
                    array[rowIndex, colIndex] = @char;
                }
            }

            return array;
        }
    }
}
