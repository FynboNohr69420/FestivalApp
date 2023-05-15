﻿using System;
using Common.Model;
namespace Server.Repositories
{
    public interface IBruger
    {
        Bruger[] getAll();
        void Add(Bruger bruger);
        void DeleteBruger(int Id);
        void UpdateBruger(Bruger bruger);
        Bruger GetBruger(int brugerID);

    }
}

