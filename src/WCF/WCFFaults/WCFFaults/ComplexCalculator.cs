using System;
using System.ServiceModel;

namespace WCFFaults
{
    [GlobalErrorHandlerBehaviour(typeof(GlobalErrorHandler))]
    public class ComplexCalculator : IComplexCalculator
    {
        public int Divide_1(int Left, int Right)
        {
            return Left / Right;
        }

        public int Divide_2(int Left, int Right)
        {
            if (Right == 0)
                throw new FaultException("Divide by zero", new FaultCode("DivideZero"));

            return Left / Right;
        }
    }
}
