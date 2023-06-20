namespace HttpRequests.CollectibleFormats {
    public class TokenBody {
        public string session;
        public UserToken[] tokens;

        public TokenBody(string session, UserToken[] tokens) {
            this.session = session;
            this.tokens = tokens;
        }
    }

    [System.Serializable]
    public class UserToken {
        public int tokenId;
        public int amount;

        public UserToken(int tokenId, int amount) {
            this.tokenId = tokenId;
            this.amount = amount;
        }
    }
}