using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dame_italienesti
{
    class Game
    {
        Player playerAlb;
        Player playerNegru;
        Piesa[,] tabla;
        Culoare randJoc;
        const int dimensiuneJoc = 8;
        public Game()
        {
            randJoc = Culoare.alb;
            playerAlb = new Player(Culoare.alb, 12);
            playerNegru = new Player(Culoare.negru, 12);
            tabla = new Piesa[dimensiuneJoc, dimensiuneJoc];
            InitializareTablaJoc();
        }
        public int GetNumarPieseAlb()
        {
            return playerAlb.GetNumarPiese();
        }
        public int GetNumarPieseNegre()
        {
            return playerNegru.GetNumarPiese();
        }
        public int GetDimensiuneJoc()
        {
            return dimensiuneJoc;
        }
        public Culoare GetRandJoc()
        {
            return randJoc;
        }
        private void SchimbaRandJoc()
        {
            if (randJoc == Culoare.alb)
            {
                randJoc = Culoare.negru;
            }
            else
            {
                randJoc = Culoare.alb;
            }
        }
        private void InitializareTablaJoc()
        {
            int contorPieseAlbePuse = 0;
            int contorPieseNegrePuse = 0;
            for (int i = 0; i < dimensiuneJoc; i++)
            {
                for (int j = 0; j < dimensiuneJoc; j++)
                {
                    tabla[i, j] = new Piesa(Culoare.gol, TipPiesa.gol);
                    tabla[i, j].SetPosition(Tuple.Create(i, j));

                    if (i == 0 || i == 2)
                    {
                        if (j % 2 == 0)
                        {
                            playerNegru.GetPiese()[contorPieseNegrePuse].SetPosition(Tuple.Create(i, j));
                            tabla[i, j] = playerNegru.GetPiese()[contorPieseNegrePuse];

                            contorPieseNegrePuse++;
                        }
                    }               
                    if (i == 1)
                    {
                        if (j % 2 != 0)
                        {
                            playerNegru.GetPiese()[contorPieseNegrePuse].SetPosition(Tuple.Create(i, j));
                            tabla[i, j] = playerNegru.GetPiese()[contorPieseNegrePuse];

                            contorPieseNegrePuse++;
                        }
                    }
                    if (i == 5 || i == 7)
                    {
                        if (j % 2 != 0)
                        {
                            playerAlb.GetPiese()[contorPieseAlbePuse].SetPosition(Tuple.Create(i, j));
                            tabla[i, j] = playerAlb.GetPiese()[contorPieseAlbePuse];

                            contorPieseAlbePuse++;
                        }
                    }   
                    if (i == 6)
                    {
                        if (j % 2 == 0)
                        {
                            playerAlb.GetPiese()[contorPieseAlbePuse].SetPosition(Tuple.Create(i, j));
                            tabla[i, j] = playerAlb.GetPiese()[contorPieseAlbePuse];

                            contorPieseAlbePuse++;
                        }  
                    }    
                }
            }
            CalculatePossibleMovesForPlayer(playerAlb);
            CalculatePossibleMovesForPlayer(playerNegru);
        }
        public Piesa[,] GetTablaJoc()
        {
            return tabla;
        }
        public bool TryMakeMove(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            if (ValidMove(oldPosition, newPosition))
            {
                MovePieceToEmptySpace(oldPosition, newPosition);
                SchimbaRandJoc();

                CalculatePossibleMovesForPlayer(playerAlb);
                CalculatePossibleMovesForPlayer(playerNegru);

                return true;
            }
            return false;
        }
        private void MovePieceToEmptySpace(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            Piesa temporar = tabla[newPosition.Item1, newPosition.Item2];
            tabla[newPosition.Item1, newPosition.Item2] = tabla[oldPosition.Item1, oldPosition.Item2];
            tabla[oldPosition.Item1, oldPosition.Item2] = temporar;

            tabla[newPosition.Item1, newPosition.Item2].SetPosition(newPosition);
            tabla[oldPosition.Item1, oldPosition.Item2].SetPosition(oldPosition);

            TryPromote(tabla[newPosition.Item1, newPosition.Item2]);
        }

        private void TryPromote(Piesa piesa)
        {
            if (piesa.GetTipPiesa() == TipPiesa.man)
            {
                if (piesa.GetPosition().Item1 == 0 && piesa.GetCuloare() == Culoare.alb ||
                    piesa.GetPosition().Item1 == 7 && piesa.GetCuloare() == Culoare.negru)
                {
                    piesa.SetTipPiesa(TipPiesa.king);
                }
            } 
        }

        private bool ValidMove(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            return tabla[oldPosition.Item1, oldPosition.Item2].GetPossibleMoves().Contains(newPosition);
        }
        private void CalculatePossibleMovesForPlayer(Player player)
        {
            foreach (Piesa piesa in player.GetPiese())
            {
                CalculatePossibleMoves(piesa);
            }
        }
        private void CalculatePossibleMoves(Piesa piesa)
        {
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();
            if (piesa.GetCuloare() == randJoc)
            {
                if (piesa.GetTipPiesa() == TipPiesa.man)
                {
                    if (piesa.GetCuloare() == Culoare.alb)
                    {
                        if (piesa.GetLinie() > 0 && piesa.GetColoana() > 0 && piesa.GetColoana() < 7)
                        {
                            if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() - 1, piesa.GetColoana() - 1));
                            }
                            if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() - 1, piesa.GetColoana() + 1));
                            }
                        }
                        else if (piesa.GetColoana() == 7)
                        {
                            if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() - 1, piesa.GetColoana() - 1));
                            }
                        }
                        else if (piesa.GetColoana() == 0)
                        {
                            if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() - 1, piesa.GetColoana() + 1));
                            }
                        }
                    }
                    else if (piesa.GetCuloare() == Culoare.negru)
                    {
                        if (piesa.GetLinie() < 7 && piesa.GetColoana() > 0 && piesa.GetColoana() < 7)
                        {
                            if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() + 1, piesa.GetColoana() - 1));
                            }
                            if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() + 1, piesa.GetColoana() + 1));
                            }
                        }
                        else if (piesa.GetColoana() == 7)
                        {
                            if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() + 1, piesa.GetColoana() - 1));
                            }
                        }
                        else if (piesa.GetColoana() == 0)
                        {
                            if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(piesa.GetLinie() + 1, piesa.GetColoana() + 1));
                            }
                        }
                    }
                }
            } 
            piesa.SetPossbileMoves(possibleMoves);
        }
    }
}
