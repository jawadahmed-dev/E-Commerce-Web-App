﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.Common
{
	public class BaseEntity
	{
		//[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
	}
}