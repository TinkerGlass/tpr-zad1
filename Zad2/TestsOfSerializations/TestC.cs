﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestsOfSerializations
{
    [Serializable]
    public class TestC
    {
        private static int nextId = 0;
        
        public TestA TestA { get; set; }
        
        public int Id { get; set; }

        public TestC()
        {
            TestA = null;
        }


        public TestC(TestA test)
        {
            TestA = test;
        }


        public override bool Equals(Object obj)
        {
            if (!(obj is TestC)) return false;
            return ((TestC)obj).Id == Id && ((TestC)obj).TestA.Id == TestA.Id && ((TestC)obj).TestA.TestB.Id == TestA.TestB.Id;
        }


        private int GetNextId()
        {
            return nextId++;
        }
    }
}
