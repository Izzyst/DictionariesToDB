using System;
using System.Collections.Generic;
using App3.Models;

namespace App3.LevelStrategy
{
    public interface IStrategy
    {
        void Execute();
        DataToLevel GetWords();
    }
}