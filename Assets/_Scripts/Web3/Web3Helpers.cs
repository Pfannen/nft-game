namespace Web3Helpers {
    public struct TokenIdentifier {
        int tokenId;
        CollectionIdentifier collection;
    }

    public enum CollectionIdentifier {
        Macho,
        Smol
    }

    [System.Serializable]
    public class Smol {
        public string tokenId { get; set; }
        public Attributes attributes { get; set; }
    }

    [System.Serializable]
    public class Attributes {
        public string Background { get; set; }

        public string Body { get; set; } = "brown";

        public string Clothes { get; set; }

        public string Glasses { get; set; }

        public string Hat { get; set; }

        public string Hair { get; set; }

        public string Mouth { get; set; }

        public string Gender { get; set; }
    }
}