using System;
using System.Collections.Generic;

namespace dame_italienesti
{
    class Game
    {
        Player playerAlb;
        Player playerNegru;
        Piesa[,] tabla;
        Culoare randJoc;
        const int dimensiuneJoc = 8;
        bool gameEnded = false;
        int piecesTaken = 0;
        public Game()
        {
            randJoc = Culoare.alb;
            playerAlb = new Player(Culoare.alb, 12);
            playerNegru = new Player(Culoare.negru, 12);
            tabla = new Piesa[dimensiuneJoc, dimensiuneJoc];
            InitializareTablaJoc();
        }
        public bool CheckGameEnded()
        {
            return gameEnded;
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
            piecesTaken = 0;
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

            CalculateAttackMovesForPlayer(playerAlb);
            CalculateAttackMovesForPlayer(playerNegru);

            CalculatePossibleMovesForPlayer(playerAlb);
            CalculatePossibleMovesForPlayer(playerNegru);
        }
        public Piesa[,] GetTablaJoc()
        {
            return tabla;
        }

        public void TryMakeMovePC()
        {
            Random random = new Random();
            if (MustAttack(randJoc))
            {
                List<Piesa> pieseCarePotAtaca = new List<Piesa>();
                foreach (Piesa piese in playerNegru.GetPiese())
                {
                    if (piese.GetAttackMoves().Count > 0)
                    {
                        pieseCarePotAtaca.Add(piese);
                    }
                }

                Piesa aleasa = pieseCarePotAtaca[random.Next(pieseCarePotAtaca.Count)];

                Tuple<int, int> oldPosition = aleasa.GetPosition();
                Tuple<int, int> newPosition = aleasa.GetAttackMoves()[random.Next(aleasa.GetAttackMoves().Count)];

                MoveAndTakePiece(oldPosition, newPosition);
                if (!GameHasEnded())
                {
                    if (PieceCanStillCapture(newPosition))
                    {
                        ContinueAttack(newPosition);
                    }
                    else
                    {
                        SchimbaRandJoc();

                        CalculateAttackMovesForPlayer(playerAlb);
                        CalculateAttackMovesForPlayer(playerNegru);

                        CalculatePossibleMovesForPlayer(playerAlb);
                        CalculatePossibleMovesForPlayer(playerNegru);
                    }
                }
            }
            else //mutare
            {
                List<Piesa> pieseCarePotMuta = new List<Piesa>();
                foreach (Piesa piese in playerNegru.GetPiese())
                {
                    if (piese.GetPossibleMoves().Count > 0)
                    {
                        pieseCarePotMuta.Add(piese);
                    }
                }

                Piesa aleasa = pieseCarePotMuta[random.Next(pieseCarePotMuta.Count)];

                Tuple<int, int> oldPosition = aleasa.GetPosition();
                Tuple<int, int> newPosition = aleasa.GetPossibleMoves()[random.Next(aleasa.GetPossibleMoves().Count)];

                MovePieceToEmptySpace(oldPosition, newPosition);
                if (!GameHasEnded())
                {
                    SchimbaRandJoc();

                    CalculateAttackMovesForPlayer(playerAlb);
                    CalculateAttackMovesForPlayer(playerNegru);

                    CalculatePossibleMovesForPlayer(playerAlb);
                    CalculatePossibleMovesForPlayer(playerNegru);
                }
            }
        }

        public bool TryMakeMove(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            if (MustAttack(randJoc))
            {
                if (ValidAttackMove(oldPosition, newPosition))
                {
                    MoveAndTakePiece(oldPosition, newPosition);
                    if (!GameHasEnded())
                    {
                        if (PieceCanStillCapture(newPosition))
                        {
                            ContinueAttack(newPosition);
                        }
                        else
                        {
                            SchimbaRandJoc();

                            CalculateAttackMovesForPlayer(playerAlb);
                            CalculateAttackMovesForPlayer(playerNegru);

                            CalculatePossibleMovesForPlayer(playerAlb);
                            CalculatePossibleMovesForPlayer(playerNegru);
                        }
                    }
                    return true;
                }
            }
            else if(ValidMove(oldPosition, newPosition))
            {
                MovePieceToEmptySpace(oldPosition, newPosition);
                if (!GameHasEnded())
                {
                    SchimbaRandJoc();

                    CalculateAttackMovesForPlayer(playerAlb);
                    CalculateAttackMovesForPlayer(playerNegru);

                    CalculatePossibleMovesForPlayer(playerAlb);
                    CalculatePossibleMovesForPlayer(playerNegru);
                }
                return true;
            }
            return false;
        }
        private void ContinueAttack(Tuple<int, int> newPosition)
        {
            Piesa careAtaca = tabla[newPosition.Item1, newPosition.Item2];
            foreach (Piesa piesa in playerAlb.GetPiese())
            {
                if (!piesa.Equals(careAtaca))
                {
                    piesa.SetAttackMoves(new List<Tuple<int, int>>());
                    piesa.SetPossbileMoves(new List<Tuple<int, int>>());
                }
            }
            foreach (Piesa piesa in playerNegru.GetPiese())
            {
                if (!piesa.Equals(careAtaca))
                {
                    piesa.SetAttackMoves(new List<Tuple<int, int>>());
                    piesa.SetPossbileMoves(new List<Tuple<int, int>>());
                }
            }
        }
        private bool PieceCanStillCapture(Tuple<int, int> newPosition)
        {
            if (piecesTaken < 3)
            {
                Piesa deVerif = tabla[newPosition.Item1, newPosition.Item2];
                CalculateAttackMoves(deVerif);
                if (deVerif.GetAttackMoves().Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private bool GameHasEnded()
        {
            if (playerAlb.GetPiese().Count == 0)
            {
                gameEnded = true;
                return true;
            }
            if (playerNegru.GetPiese().Count == 0)
            {
                gameEnded = true;
                return true;
            }
            return false;
        }
        private void MoveAndTakePiece(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            Piesa temporar = tabla[newPosition.Item1, newPosition.Item2];
            tabla[newPosition.Item1, newPosition.Item2] = tabla[oldPosition.Item1, oldPosition.Item2];
            tabla[oldPosition.Item1, oldPosition.Item2] = temporar;

            tabla[newPosition.Item1, newPosition.Item2].SetPosition(newPosition);
            tabla[oldPosition.Item1, oldPosition.Item2].SetPosition(oldPosition);

            Tuple<int, int> middlePosition = CalculateMiddlePosition(oldPosition, newPosition);
            RemovePiece(middlePosition);
            piecesTaken++;
            TryPromote(tabla[newPosition.Item1, newPosition.Item2]);
        }
        private void RemovePiece(Tuple<int, int> middlePosition)
        {
            Piesa deSters = tabla[middlePosition.Item1, middlePosition.Item2];
            if (deSters.GetCuloare() == Culoare.negru)
            {
                playerNegru.GetPiese().Remove(deSters);
            }
            else
            {
                playerAlb.GetPiese().Remove(deSters);
            }
            tabla[middlePosition.Item1, middlePosition.Item2] = new Piesa();
            tabla[middlePosition.Item1, middlePosition.Item2].SetPosition(middlePosition);
        }
        private Tuple<int, int> CalculateMiddlePosition(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            if ((oldPosition.Item1 - newPosition.Item1) > 0) //mai sus
            {
                if ((oldPosition.Item2 - newPosition.Item2) > 0) //stanga
                {
                    return Tuple.Create(oldPosition.Item1 - 1, oldPosition.Item2 - 1);
                }
                else //dreapta
                {
                    return Tuple.Create(oldPosition.Item1 - 1, oldPosition.Item2 + 1);
                }
            }
            else //mai jos
            {
                if ((oldPosition.Item2 - newPosition.Item2) > 0) //stanga
                {
                    return Tuple.Create(oldPosition.Item1 + 1, oldPosition.Item2 - 1);
                }
                else //dreapta
                {
                    return Tuple.Create(oldPosition.Item1 + 1, oldPosition.Item2 + 1);
                }
            }
        }
        private bool ValidAttackMove(Tuple<int, int> oldPosition, Tuple<int, int> newPosition)
        {
            return tabla[oldPosition.Item1, oldPosition.Item2].GetAttackMoves().Contains(newPosition);
        }
        private bool MustAttack(Culoare randJoc)
        {
            if (randJoc == Culoare.alb)
            {
                foreach (Piesa piesa in playerAlb.GetPiese())
                {
                    if (piesa.GetAttackMoves().Count > 0)
                    {
                        return true;
                    }
                }
            }
            else if (randJoc == Culoare.negru)
            {
                foreach (Piesa piesa in playerNegru.GetPiese())
                {
                    if (piesa.GetAttackMoves().Count > 0)
                    {
                        return true;
                    }
                }
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
        private void CalculateAttackMovesForPlayer(Player player)
        {
            foreach (Piesa piesa in player.GetPiese())
            {
                CalculateAttackMoves(piesa);
            }
        }
        private void CalculateAttackMoves(Piesa piesa)
        {
            List<Tuple<int, int>> captures = new List<Tuple<int, int>>();
            if (piesa.GetCuloare() == randJoc)
            {
                if (piesa.GetTipPiesa() == TipPiesa.man)
                {
                    if (piesa.GetCuloare() == Culoare.alb)
                    {
                        if (piesa.GetLinie() > 1)
                        {
                            if (piesa.GetColoana() > 1)
                            {
                                if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.negru && 
                                    tabla[piesa.GetLinie() - 1, piesa.GetColoana() - 1].GetTipPiesa() == TipPiesa.man)
                                {
                                    int i = piesa.GetLinie() - 2;
                                    int j = piesa.GetColoana() - 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                            if (piesa.GetColoana() < 6)
                            {
                                if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.negru &&
                                    tabla[piesa.GetLinie() - 1, piesa.GetColoana() + 1].GetTipPiesa() == TipPiesa.man)
                                {
                                    int i = piesa.GetLinie() - 2;
                                    int j = piesa.GetColoana() + 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                        }
                    }
                    else if(piesa.GetCuloare() == Culoare.negru)
                    {
                        if (piesa.GetLinie() < 6)
                        {
                            if (piesa.GetColoana() > 1)
                            {
                                if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.alb &&
                                    tabla[piesa.GetLinie() + 1, piesa.GetColoana() - 1].GetTipPiesa() == TipPiesa.man)
                                {
                                    int i = piesa.GetLinie() + 2;
                                    int j = piesa.GetColoana() - 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                            if (piesa.GetColoana() < 6)
                            {
                                if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.alb &&
                                    tabla[piesa.GetLinie() + 1, piesa.GetColoana() + 1].GetTipPiesa() == TipPiesa.man)
                                {
                                    int i = piesa.GetLinie() + 2;
                                    int j = piesa.GetColoana() + 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                        }
                    }
                }
                else if (piesa.GetTipPiesa() == TipPiesa.king)
                {
                    if (piesa.GetCuloare() == Culoare.alb)
                    {
                        if (piesa.GetLinie() > 1)
                        {
                            if (piesa.GetColoana() > 1)
                            {
                                if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.negru)
                                {
                                    int i = piesa.GetLinie() - 2;
                                    int j = piesa.GetColoana() - 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                            if (piesa.GetColoana() < 6)
                            {
                                if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.negru)
                                {
                                    int i = piesa.GetLinie() - 2;
                                    int j = piesa.GetColoana() + 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                        }
                        if (piesa.GetLinie() < 6)
                        {
                            if (piesa.GetColoana() > 1)
                            {
                                if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.negru)
                                {
                                    int i = piesa.GetLinie() + 2;
                                    int j = piesa.GetColoana() - 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                            if (piesa.GetColoana() < 6)
                            {
                                if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.negru)
                                {
                                    int i = piesa.GetLinie() + 2;
                                    int j = piesa.GetColoana() + 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                        }
                    }
                    else if (piesa.GetCuloare() == Culoare.negru)
                    {
                        if (piesa.GetLinie() > 1)
                        {
                            if (piesa.GetColoana() > 1)
                            {
                                if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.alb)
                                {
                                    int i = piesa.GetLinie() - 2;
                                    int j = piesa.GetColoana() - 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                            if (piesa.GetColoana() < 6)
                            {
                                if (tabla[piesa.GetLinie() - 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.alb)
                                {
                                    int i = piesa.GetLinie() - 2;
                                    int j = piesa.GetColoana() + 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                        }
                        if (piesa.GetLinie() < 6)
                        {
                            if (piesa.GetColoana() > 1)
                            {
                                if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() - 1].GetCuloare() == Culoare.alb)
                                {
                                    int i = piesa.GetLinie() + 2;
                                    int j = piesa.GetColoana() - 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                            if (piesa.GetColoana() < 6)
                            {
                                if (tabla[piesa.GetLinie() + 1, piesa.GetColoana() + 1].GetCuloare() == Culoare.alb)
                                {
                                    int i = piesa.GetLinie() + 2;
                                    int j = piesa.GetColoana() + 2;
                                    if (tabla[i, j].GetCuloare() == Culoare.gol)
                                    {
                                        captures.Add(Tuple.Create(i, j));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            piesa.SetAttackMoves(captures);
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
                        if (piesa.GetLinie() > 0)
                        {
                            if (piesa.GetColoana() > 0)
                            {
                                int i = piesa.GetLinie() - 1;
                                int j = piesa.GetColoana() - 1;
                                if (tabla[i, j].GetCuloare() == Culoare.gol)
                                {
                                    possibleMoves.Add(Tuple.Create(i, j));
                                }
                            }
                            if (piesa.GetColoana() < 7)
                            {
                                int i = piesa.GetLinie() - 1;
                                int j = piesa.GetColoana() + 1;
                                if (tabla[i, j].GetCuloare() == Culoare.gol)
                                {
                                    possibleMoves.Add(Tuple.Create(i, j));
                                }
                            }
                        }
                    }
                    else if (piesa.GetCuloare() == Culoare.negru)
                    {
                        if (piesa.GetLinie() < 7)
                        {
                            if (piesa.GetColoana() > 0)
                            {
                                int i = piesa.GetLinie() + 1;
                                int j = piesa.GetColoana() - 1;
                                if (tabla[i, j].GetCuloare() == Culoare.gol)
                                {
                                    possibleMoves.Add(Tuple.Create(i, j));
                                }
                            }
                            if (piesa.GetColoana() < 7)
                            {
                                int i = piesa.GetLinie() + 1;
                                int j = piesa.GetColoana() + 1;
                                if (tabla[i, j].GetCuloare() == Culoare.gol)
                                {
                                    possibleMoves.Add(Tuple.Create(i, j));
                                }
                            }
                        }
                    }
                }
                else if (piesa.GetTipPiesa() == TipPiesa.king)
                {
                    if (piesa.GetLinie() < 7)
                    {
                        if (piesa.GetColoana() > 0)
                        {
                            int i = piesa.GetLinie() + 1;
                            int j = piesa.GetColoana() - 1;
                            if (tabla[i, j].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(i, j));
                            }
                        }
                        if (piesa.GetColoana() < 7)
                        {
                            int i = piesa.GetLinie() + 1;
                            int j = piesa.GetColoana() + 1;
                            if (tabla[i, j].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(i, j));
                            }
                        }
                    }
                    if(piesa.GetLinie() > 0)
                    {
                        if (piesa.GetColoana() > 0)
                        {
                            int i = piesa.GetLinie() - 1;
                            int j = piesa.GetColoana() - 1;
                            if (tabla[i, j].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(i, j));
                            }
                        }
                        if (piesa.GetColoana() < 7)
                        {
                            int i = piesa.GetLinie() - 1;
                            int j = piesa.GetColoana() + 1;
                            if (tabla[i, j].GetCuloare() == Culoare.gol)
                            {
                                possibleMoves.Add(Tuple.Create(i, j));
                            }
                        }
                    }
                }
            }
            piesa.SetPossbileMoves(possibleMoves);
        }
    }
}
