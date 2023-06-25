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

    //Must be public fields instead of properties for proper value saving.
    [System.Serializable]
    public class Attributes {

        public string Background;

        public string Body;

        public string Clothes;

        public string Glasses;

        public string Hat;

        public string Hair;

        public string Mouth;

        public string Gender;
    }
}