using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dame_italienesti
{
    class Piesa
    {
        Culoare culoare;
        TipPiesa tip;
        Tuple<int, int> position;
        List<Tuple<int, int>> posibleMoves;
        List<Tuple<int, int>> attackMoves;
        public Piesa()
        {
            culoare = Culoare.gol;
            tip = TipPiesa.gol;
            position = Tuple.Create(0, 0);
            posibleMoves = new List<Tuple<int, int>>();
            attackMoves = new List<Tuple<int, int>>();
        }
        public Piesa(Culoare culoare, TipPiesa tip)
        {
            this.tip = tip;
            this.culoare = culoare;
            position = Tuple.Create(0, 0);
        }
        public Culoare GetCuloare()
        {
            return culoare;
        }
        public void SetCuloare(Culoare culoare)
        {
            this.culoare = culoare;
        }
        public int GetLinie()
        {
            return position.Item1;
        }
        public int GetColoana()
        {
            return position.Item2;
        }
        public void SetPosition(Tuple<int, int> pozitie)
        {
            this.position = pozitie;
        }
        public Tuple<int, int> GetPosition()
        {
            return position;
        }
        public List<Tuple<int, int>> GetPossibleMoves()
        {
            return posibleMoves;
        }
        public void SetPossbileMoves(List<Tuple<int, int>> moves)
        {
            posibleMoves = moves;
        }
        public TipPiesa GetTipPiesa()
        {
            return tip;
        }
        public void SetTipPiesa(TipPiesa tipNou)
        {
            tip = tipNou;
        }
        public List<Tuple<int, int>> GetAttackMoves()
        {
            return attackMoves;
        }
        public void SetAttackMoves(List<Tuple<int, int>> moves)
        {
            attackMoves = moves;
        }
    }
}
