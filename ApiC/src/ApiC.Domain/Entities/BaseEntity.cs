using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiC.Domain.Entities
{
    public abstract class BaseEntity
    {
		[Key]
        public Guid Id { get; set; }

		private DateTime? createAt;

		public DateTime? CreateAt
		{
			get { return createAt; }
			set { createAt = (value == null ? DateTime.UtcNow : value); }
		}


		public DateTime? UpdateAt { get; set; }
	}
}
