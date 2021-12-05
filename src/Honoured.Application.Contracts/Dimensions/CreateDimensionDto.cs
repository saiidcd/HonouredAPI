namespace Honoured.Dimensions
{
    public class CreateDimensionDto
    {
        #region Props
        public string Name { get; set; }

        public decimal MinHeight { get; set; }

        public decimal MinWidth { get; set; }

        public decimal MaxHeight { get; set; }

        public decimal MaxWidth { get; set; }
        #endregion Props
    }
}
