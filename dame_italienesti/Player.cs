using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dame_italienesti
{
    class Player
    {
        Culoare culoare;
        List<Piesa> piese;
        public Player(Culoare culoare, int nrPiese)
        {
            piese = new List<Piesa>();
            this.culoare = culoare;
            for (int i = 0; i < nrPiese; i++)
            {
                Piesa p = new Piesa(this.culoare, TipPiesa.man);
                piese.Add(p);
            }
        }
        public List<Piesa> GetPiese()
        {
            return piese;
        }
        public Culoare GetCuloare()
        {
            return culoare;
        }
        public int GetNumarPiese()
        {
            return piese.Count;
        }
    }
}
