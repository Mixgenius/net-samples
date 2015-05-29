using System;

namespace Model.PdfViewModels
{
    public class ProductItem : IEquatable<ProductItem>
    {
        private readonly bool _shouldSerializeName;
        private readonly bool _shouldSerializeReference;
        private readonly bool _shouldSerializeDescription;

        public ProductItem(bool shouldSerializeName, bool shouldSerializeReference, bool shouldSerializeDescription)
        {
            _shouldSerializeName = shouldSerializeName;
            _shouldSerializeReference = shouldSerializeReference;
            _shouldSerializeDescription = shouldSerializeDescription;
        }

        public bool ShouldSerializeName { get { return _shouldSerializeName; } }
        public bool ShouldSerializeReference { get { return _shouldSerializeReference; } }
        public bool ShouldSerializeDescription { get { return _shouldSerializeDescription; } }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool Equals(ProductItem other)
        {
            if (null == other)
                return false;

            return Id == other.Id;
        }
    }
}