using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public abstract class BaseDistribution 
    {
        protected const string INVALID_OPERATION_MSG = "This type of random variable is not supported by this distribution";

        public virtual int CalcNextInt(System.Random uniformRandom)
        {
            throw new InvalidOperationException(INVALID_OPERATION_MSG);
        }
        public virtual double CalcNextDouble(System.Random uniformRandom)
        {
            throw new InvalidOperationException(INVALID_OPERATION_MSG);
        }

        public virtual float CalcNextSingle(System.Random uniformRandom)
        {
            return (float)CalcNextDouble(uniformRandom);
        }
    }
}
