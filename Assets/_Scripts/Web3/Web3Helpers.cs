namespace Web3Helpers {
    public struct TokenIdentifier {
        int tokenId;
        CollectionIdentifier collection;
    }

    public enum CollectionIdentifier {
        Macho,
        Smol
    }

    public static class LayerHelper {
        public static int NumLayers(CollectionIdentifier collection) {
            if (collection == CollectionIdentifier.Smol) return 7;
            if (collection == CollectionIdentifier.Macho) return 5;
            return -1;
        }
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