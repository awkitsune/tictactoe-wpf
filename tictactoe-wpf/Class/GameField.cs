using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_wpf.Class
{
    class GameField
    {
        const float freeCellWeight = 0.7f;
        const float winCellWeight = 0.5f;
        const float suppressCellWeight = 0.3f;
        const float neutralWeight = 0.0f;

        public const int playerMark = -1;
        public const int aiMark = 1;
        public const int neutralMark = 0;

        private const long moveDelay = 500;

        public int[][] field = 
            Enumerable.Repeat(Enumerable.Repeat(neutralMark, 3).ToArray(), 3).ToArray();
        private float[][] weights =
            Enumerable.Repeat(Enumerable.Repeat(neutralWeight, 3).ToArray(), 3).ToArray();

        public bool isWon = true;
        public bool isPlayerWon = false;

        public void Flush()
        {
            field = 
                Enumerable.Repeat(Enumerable.Repeat(neutralMark, 3).ToArray(), 3).ToArray();
            FlushWeights();
        }

        private void FlushWeights()
        {
            weights =
                Enumerable.Repeat(Enumerable.Repeat(neutralWeight, 3).ToArray(), 3).ToArray();
        }

        public int WhoWin()
        {
            if (isWon && isPlayerWon) return playerMark;
            if (isWon && !isPlayerWon) return playerMark;

            return neutralMark;
        }

        private void AiDraw(Pair<int, int> coordinates)
        {
            if (field[coordinates.First][coordinates.Second] == neutralMark)
                field[coordinates.First][coordinates.Second] = aiMark;
        }

        public void PlayerDraw(Pair<int, int> coordinates)
        {
            if (field[coordinates.First][coordinates.Second] == neutralMark)
                field[coordinates.First][coordinates.Second] = playerMark;
            SetWinStates(playerMark);
        }

        private void AiMove()
        {
            var coordinatesForMove = new Pair<int, int>(0, 0);
            var maximimWeight = 0.1f;

            FindPosition();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (weights[i][j] > maximimWeight)
                    {
                        maximimWeight = weights[i][j];
                        coordinatesForMove = new Pair<int, int>(i, j);
                    }
                }
            }
            AiDraw(coordinatesForMove);

            SetWinStates(aiMark);
            FlushWeights();
        }

        private void FindPosition()
        {
            InitializeWeights();

            SearchInLines(playerMark);
            SearchInLines(aiMark);

            RandomizeWeights();
            NormalizeWeights();
        }

        private void NormalizeWeights()
        {
            var min = float.MaxValue;
            var max = float.MinValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (weights[i][j] < min) min = weights[i][j];
                    if (weights[i][j] > max) max = weights[i][j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    weights[i][j] = Utilities.Normalize(weights[i][j], min, max);
                }
            }
        }

        private void RandomizeWeights()
        {
            var rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i][j] == neutralMark) 
                        weights[i][j] += (float)rnd.NextDouble() / 100.0f;
                }
            }
        }

        private void SearchInLines(int playerType)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i][0] + field[i][1] + field[i][2] == playerType << 1
                        && weights[i][j] > 0.2f)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (field[i][j] == neutralMark)
                            {
                                switch (playerType)
                                {
                                    case playerMark:
                                        weights[i][k] += suppressCellWeight;
                                        break;
                                    case aiMark:
                                        weights[i][k] += winCellWeight;
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[0][i] + field[1][i] + field[2][i] == playerType << 1
                        && weights[i][j] > 0.2f)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            if (field[i][j] == neutralMark)
                            {
                                switch (playerType)
                                {
                                    case playerMark:
                                        weights[k][i] += suppressCellWeight;
                                        break;
                                    case aiMark:
                                        weights[k][i] += winCellWeight;
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            if (field[0][0] + field[1][1] + field[2][2] == playerType << 1)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (field[k][k] == neutralMark)
                    {
                        switch (playerType)
                        {
                            case playerMark:
                                weights[k][k] += suppressCellWeight;
                                break;
                            case aiMark:
                                weights[k][k] += winCellWeight;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            if (field[2][0] + field[1][1] + field[0][2] == playerType << 1)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (field[2 - k][k] == neutralMark)
                    {
                        switch (playerType)
                        {
                            case playerMark:
                                weights[2 - k][k] += suppressCellWeight;
                                break;
                            case aiMark:
                                weights[2 - k][k] += winCellWeight;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void InitializeWeights()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[j][i] == neutralMark)
                    {
                        weights[j][i] = freeCellWeight;
                    }
                }
            }
        }
        private void SetWinStates(int playerType)
        {
            if (CheckForWin(playerType))
            {
                isWon = true;
                switch (playerType)
                {
                    case aiMark:
                        isPlayerWon = false;
                        break;
                    case playerMark:
                        isPlayerWon = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                isWon = false;
                isPlayerWon = false;

                switch (playerType)
                {
                    case playerMark:
                        AiMove();
                        break;
                    default:
                        break;
                }
            }
        }

        private bool CheckForWin(int playerType)
        {
            if ((field[0][0] == playerType && field[1][0] == playerType && field[2][0] == playerType) ||
            (field[0][1] == playerType && field[1][1] == playerType && field[2][1] == playerType) ||
            (field[0][2] == playerType && field[1][2] == playerType && field[2][2] == playerType))
            {
                return true;
            }

            if ((field[0][0] == playerType && field[0][1] == playerType && field[0][2] == playerType) ||
                (field[1][0] == playerType && field[1][1] == playerType && field[1][2] == playerType) ||
                (field[2][0] == playerType && field[2][1] == playerType && field[2][2] == playerType))
            {
                return true;
            }

            if ((field[0][0] == playerType && field[1][1] == playerType && field[2][2] == playerType) ||
                (field[2][0] == playerType && field[1][1] == playerType && field[0][2] == playerType))
            {
                return true;
            }

            return false;
        }
    }
}
