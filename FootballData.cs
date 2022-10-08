using FootballSphere.Geometry;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballSphere
{
    internal class FootballData
    {
        private float angle;
        private bool cheatMode;
        private int n;
        private int m;
        private int k, K;
        public LLtude[] nodes;
        public int[][] frames;
        public List<int>[] shells;

        private void CreateNodes()
        {
            for (int i = 0; i < n; i++)
            {
                nodes[i] = Calculator.AngularRatioPoint(
                    Calculator.a1x, Calculator.a1, 1f / n * (i + 1));
            }
            for (int i = 0; i < m; i++)
            {
                nodes[n + i] = Calculator.AngularRatioPoint(
                    Calculator.a1, Calculator.a2, 1f / m * (i + 1));
            }
            for (int i = 0; i < n; i++)
            {
                nodes[n + m + i] = Calculator.AngularRatioPoint(
                    Calculator.a2, Calculator.a3, 1f / n * (i + 1));
            }
            for (int i = 0; i < m; i++)
            {
                nodes[2 * n + m + i] = Calculator.AngularRatioPoint(
                    Calculator.a3, Calculator.a4, 1f / m * (i + 1));
            }
            for (int i = 0; i < n - 1; i++)
            {
                nodes[2 * n + 2 * m + i] = Calculator.AngularRatioPoint(
                    Calculator.a4, Calculator.a2x, 1f / n * (i + 1));
            }
            for (int i = 0; i < n; i++)
            {
                nodes[3 * n + 2 * m - 1 + i] = Calculator.AngularRatioPoint(
                    Calculator.a3, Calculator.a5, 1f / n * (i + 1));
            }
            for (int i = 0; i < m; i++)
            {
                nodes[4 * n + 2 * m - 1 + i] = Calculator.AngularRatioPoint(
                    Calculator.a5, Calculator.a7, 1f / m * (i + 1));
            }
            for (int i = 0; i < n; i++)
            {
                nodes[4 * n + 3 * m - 1 + i] = Calculator.AngularRatioPoint(
                    Calculator.a7, Calculator.a8, 1f / n * (i + 1));
            }
            for (int i = 0; i < m; i++)
            {
                nodes[5 * n + 3 * m - 1 + i] = Calculator.AngularRatioPoint(
                    Calculator.a8, Calculator.a6, 1f / m * (i + 1));
            }
            for (int i = 0; i < n - 1; i++)
            {
                nodes[5 * n + 4 * m - 1 + i] = Calculator.AngularRatioPoint(
                    Calculator.a6, Calculator.a6x, 1f / n * (i + 1));
            }
            for (int i = 0; i < n - 1; i++)
            {
                nodes[6 * n + 4 * m - 2 + i] = Calculator.AngularRatioPoint(
                    Calculator.a6, Calculator.a4, 1f / n * (i + 1));
            }
            for (int i = 0; i < n; i++)
            {
                nodes[7 * n + 4 * m - 3 + i] = Calculator.AngularRatioPoint(
                    Calculator.a7, Calculator.a9, 1f / n * (i + 1));
            }
            for (int i = 0; i < n; i++)
            {
                nodes[8 * n + 4 * m - 3 + i] = Calculator.AngularRatioPoint(
                    Calculator.a9, Calculator.a11, 1f / n * (i + 1));
            }
            for (int i = 0; i < n; i++)
            {
                nodes[9 * n + 4 * m - 3 + i] = Calculator.AngularRatioPoint(
                    Calculator.a11, Calculator.a10, 1f / n * (i + 1));
            }
            for (int i = 0; i < n - 1; i++)
            {
                nodes[10 * n + 4 * m - 3 + i] = Calculator.AngularRatioPoint(
                    Calculator.a10, Calculator.a8, 1f / n * (i + 1));
            }
            for (int i = 0; i < m - 1; i++)
            {
                nodes[11 * n + 4 * m - 4 + i] = Calculator.AngularRatioPoint(
                    Calculator.a10, Calculator.a10x, 1f / m * (i + 1));
            }
            for (int i = 0; i < m; i++)
            {
                nodes[11 * n + 5 * m - 5 + i] = Calculator.AngularRatioPoint(
                    Calculator.a11, Calculator.a12, 1f / m * (i + 1));
            }
            for (int i = 0; i < n - 1; i++)
            {
                nodes[11 * n + 6 * m - 5 + i] = Calculator.AngularRatioPoint(
                    Calculator.a12, Calculator.a12x, 1f / n * (i + 1));
            }
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    nodes[i * k + j] = new LLtude(nodes[j].phi, nodes[j].lambda + 0.4f * i * Mathf.PI);
                }
            }
        }

        private void CreateFrames()
        {
            frames[0] = new int[n + 1];
            frames[0][0] = n - 1 + k;
            for (int j = 0; j < n; j++)
            {
                frames[0][j + 1] = j;
            }
            frames[1] = new int[m + 1];
            for (int j = 0; j < m + 1; j++)
            {
                frames[1][j] = n - 1 + j;
            }
            frames[2] = new int[n + 1];
            for (int j = 0; j < n + 1; j++)
            {
                frames[2][j] = n + m - 1 + j;
            }
            frames[3] = new int[m + 1];
            for (int j = 0; j < m + 1; j++)
            {
                frames[3][j] = 2 * n + m - 1 + j;
            }
            frames[4] = new int[n + 1];
            for (int j = 0; j < n; j++)
            {
                frames[4][j] = 2 * n + 2 * m - 1 + j;
            }
            frames[4][n] = n + m - 1 + k;
            frames[5] = new int[n + 1];
            frames[5][0] = 2 * n + m - 1;
            for (int j = 0; j < n; j++)
            {
                frames[5][j + 1] = 3 * n + 2 * m - 1 + j;
            }
            frames[6] = new int[m + 1];
            for (int j = 0; j < m + 1; j++)
            {
                frames[6][j] = 4 * n + 2 * m - 2 + j;
            }
            frames[7] = new int[n + 1];
            for (int j = 0; j < n + 1; j++)
            {
                frames[7][j] = 4 * n + 3 * m - 2 + j;
            }
            frames[8] = new int[m + 1];
            for (int j = 0; j < m + 1; j++)
            {
                frames[8][j] = 5 * n + 3 * m - 2 + j;
            }
            frames[9] = new int[n + 1];
            for (int j = 0; j < n; j++)
            {
                frames[9][j] = 5 * n + 4 * m - 2 + j;
            }
            frames[9][n] = 4 * n + 2 * m - 2 + k;
            frames[10] = new int[n + 1];
            frames[10][0] = 5 * n + 4 * m - 2;
            for (int j = 0; j < n - 1; j++)
            {
                frames[10][j + 1] = 6 * n + 4 * m - 2 + j;
            }
            frames[10][n] = 2 * n + 2 * m - 1;
            frames[11] = new int[n + 1];
            frames[11][0] = 4 * n + 3 * m - 2;
            for (int j = 0; j < n; j++)
            {
                frames[11][j + 1] = 7 * n + 4 * m - 3 + j;
            }
            frames[12] = new int[n + 1];
            for (int j = 0; j < n + 1; j++)
            {
                frames[12][j] = 8 * n + 4 * m - 4 + j;
            }
            frames[13] = new int[n + 1];
            for (int j = 0; j < n + 1; j++)
            {
                frames[13][j] = 9 * n + 4 * m - 4 + j;
            }
            frames[14] = new int[n + 1];
            for (int j = 0; j < n; j++)
            {
                frames[14][j] = 10 * n + 4 * m - 4 + j;
            }
            frames[14][n] = 5 * n + 3 * m - 2;
            frames[15] = new int[m + 1];
            frames[15][0] = 10 * n + 4 * m - 4;
            for (int j = 0; j < m - 1; j++)
            {
                frames[15][j + 1] = 11 * n + 4 * m - 4 + j;
            }
            frames[15][m] = 8 * n + 4 * m - 4 + k;
            frames[16] = new int[m + 1];
            frames[16][0] = 9 * n + 4 * m - 4;
            for (int j = 0; j < m; j++)
            {
                frames[16][j + 1] = 11 * n + 5 * m - 5 + j;
            }
            frames[17] = new int[n + 1];
            for (int j = 0; j < n; j++)
            {
                frames[17][j] = 11 * n + 6 * m - 6 + j;
            }
            frames[17][n] = 11 * n + 6 * m - 6 + k;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    frames[18 * i + j] = new int[frames[j].Length];
                    for (int h = 0; h < frames[j].Length; h++)
                    {
                        frames[18 * i + j][h] = (frames[j][h] + i * k) % K;
                    }
                }
            }
        }

        private void CreateShell()
        {
            shells = new List<int>[32];
            shells[0] = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    shells[0].Add(frames[18 * i][n - j]);
                }
            }
            shells[1] = new List<int>();
            for (int i = 0; i < n; i++)
            {
                shells[1].Add(frames[0][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[1].Add(frames[1][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[1].Add(frames[2][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[1].Add(frames[3][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[1].Add(frames[4][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[1].Add(frames[19][m - i]);
            }
            shells[2] = new List<int>();
            for (int i = 0; i < n; i++)
            {
                shells[2].Add(frames[2][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[2].Add(frames[5][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[2].Add(frames[81][n - i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[2].Add(frames[82][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[2].Add(frames[76][i]);
            }
            shells[3] = new List<int>();
            for (int i = 0; i < m; i++)
            {
                shells[3].Add(frames[3][m - i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[3].Add(frames[5][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[3].Add(frames[6][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[3].Add(frames[7][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[3].Add(frames[8][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[3].Add(frames[10][i]);
            }
            shells[4] = new List<int>();
            for (int i = 0; i < m; i++)
            {
                shells[4].Add(frames[6][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[4].Add(frames[11][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[4].Add(frames[87][m - i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[4].Add(frames[86][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[4].Add(frames[80][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[4].Add(frames[81][i]);
            }
            shells[5] = new List<int>();
            for (int i = 0; i < n; i++)
            {
                shells[5].Add(frames[11][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[5].Add(frames[12][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[5].Add(frames[13][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[5].Add(frames[14][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[5].Add(frames[7][n - i]);
            }
            shells[6] = new List<int>();
            for (int i = 0; i < n; i++)
            {
                shells[6].Add(frames[12][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[6].Add(frames[16][i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[6].Add(frames[89][n - i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[6].Add(frames[88][m - i]);
            }
            for (int i = 0; i < n; i++)
            {
                shells[6].Add(frames[85][i]);
            }
            for (int i = 0; i < m; i++)
            {
                shells[6].Add(frames[87][i]);
            }
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    shells[1 + i * 6 + j] = new List<int>();
                    foreach (var order in shells[j + 1])
                    {
                        shells[1 + i * 6 + j].Add((order + i * k) % K);
                    }
                }
            }
            shells[31] = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    shells[31].Add(frames[17 + i * 18][j]);
                }
            }
        }

        public FootballData(float angle, bool cheatMode)
        {
            this.cheatMode = cheatMode;
            this.angle = _in_range(cheatMode, angle);
            n = Mathf.FloorToInt(Calculator.bigAngle / this.angle);
            m = Mathf.FloorToInt(Calculator.smallAngle / this.angle);
            k = 12 * n + 6 * m - 6;
            K = 5 * k;
            nodes = new LLtude[K];
            frames = new int[90][];
            shells = new List<int>[32];
            CreateNodes();
            CreateFrames();
            CreateShell();
        }

        public void Draw(DysonSphereLayer layer)
        {
            bool lat75 = Mathf.RoundToInt(layer.gameData.history.dysonNodeLatitude) >= 75;
            bool lat90 = Mathf.RoundToInt(layer.gameData.history.dysonNodeLatitude) >= 90;
            Dictionary<int, int> order2index = new Dictionary<int, int>();
            float radius = layer.orbitRadius;
            if (cheatMode || lat75)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    order2index.Add(i, layer.NewDysonNode(FootballSphere.nodeProtoId, nodes[i].Position(radius)));
                }
            }
            if (cheatMode || lat75)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    for (int j = 0; j < frames[i].Length - 1; j++)
                    {
                        layer.NewDysonFrame(FootballSphere.frameProtoId, order2index[frames[i][j]], order2index[frames[i][j + 1]], false);
                    }
                }
            }
            if (cheatMode || lat90)
            {
                for (int i = 0; i < shells.Length; i++)
                {
                    List<int> shellNodes = new List<int>();
                    foreach (int order in shells[i])
                    {
                        shellNodes.Add(order2index[order]);
                    }
                    layer.NewDysonShell(FootballSphere.shellProtoId, shellNodes);
                }
            }
        }

        private static Func<bool, float, float> _in_range = (bool cheatMode, float angle) =>
        {
            if (cheatMode)
            {
                return angle < 1f ? 1f : (angle > 18f ? 18f : angle);
            }
            return angle < 4f ? 4f : (angle > 18f ? 18f : angle);
        };
    }
}
