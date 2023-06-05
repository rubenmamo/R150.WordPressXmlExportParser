namespace WordPressXmlExportParser
{
	public struct Category
    {
		public int Id { get; internal set; }

		public string NiceName { get; internal set; }

		public string Name { get; internal set; }

		public string ParentCategoryName { get; internal set; }
	}
}
