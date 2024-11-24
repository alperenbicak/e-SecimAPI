using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSecimAPI.Models
{
	public class ElectionCreateDto
	{
		[Required(ErrorMessage = "Seçim adı gereklidir.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Bitiş tarihi gereklidir.")]
		public DateTime EndDate { get; set; }

		// Adaylar isteğe bağlı gönderilebilir
		public List<string> CandidateNames { get; set; } = new List<string>();
	}
}
