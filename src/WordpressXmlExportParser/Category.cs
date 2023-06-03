using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressXmlExportParser
{
    public class Category
    {
		public int Id { get; set; }

		public string NiceName { get; set; }

		public string Name { get; set; }

		public string ParentCategoryName { get; set; }
	}
}
