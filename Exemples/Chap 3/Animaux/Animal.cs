using System;

namespace Animaux
{
    class Animal
    {
        public Int16 masse;

        protected virtual void Manger()
        {
            throw new NotImplementedException();
        }
    }
}
