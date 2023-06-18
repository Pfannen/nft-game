namespace HttpRequests.CollectibleFormats {
    public class TokenBody {
        public int accountId;
        public UserToken[] tokens;

        public TokenBody(int accountId, UserToken[] tokens) {
            this.accountId = accountId;
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