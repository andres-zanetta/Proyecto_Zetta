﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Zetta.DB.Data
{
    public class EntityBase: IEntityBase
    {
        public int  Id { get; set; }
        public bool Aceptado { get; set; }
        
            
    }   
}
