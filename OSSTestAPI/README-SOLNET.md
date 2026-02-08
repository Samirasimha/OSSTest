# Solana Integration

## Important Note on Package Choice

The original requirement specified using `SolnetUnified` version 0.0.8 from https://libraries.io/nuget/SolnetUnified/0.0.8.

However, **SolnetUnified is not available on NuGet.org**. Research indicates that this package may have been delisted due to security concerns or policy violations.

### Alternative Solution

Instead, this implementation uses the **official Solnet packages** which are:
- Actively maintained
- Security-verified (no known vulnerabilities)
- The standard for Solana blockchain integration in .NET

**Packages Used:**
- `Solnet.Wallet` version 6.1.0 - For wallet management and key generation
- `Solnet.Rpc` version 6.1.0 - For RPC interactions with Solana blockchain

## Features Implemented

### 1. Generate Solana Wallet
**Endpoint:** `GET /solana/wallet/generate`

Generates a new Solana wallet with a 12-word mnemonic phrase.

**Response Example:**
```json
{
    "success": true,
    "message": "Solana wallet generated successfully using Solnet!",
    "publicKey": "HUR2t3yFdGKEHrgM7LM7rwg1AT69pqdzgAwPhCwLKaTe",
    "mnemonicWords": "strong latin celery chuckle vote sauce cage build aerobic supreme spice case",
    "warning": "This is a demo wallet. Never use these keys in production!"
}
```

### 2. Restore Solana Wallet
**Endpoint:** `POST /solana/wallet/restore`

Restores a wallet from a mnemonic phrase.

**Request Body:**
```json
{
    "mnemonicPhrase": "strong latin celery chuckle vote sauce cage build aerobic supreme spice case"
}
```

**Response Example:**
```json
{
    "success": true,
    "message": "Solana wallet restored successfully using Solnet!",
    "publicKey": "HUR2t3yFdGKEHrgM7LM7rwg1AT69pqdzgAwPhCwLKaTe",
    "warning": "This is a demo. Never share your mnemonic phrase!"
}
```

## Testing

You can test the endpoints using curl:

```bash
# Generate a new wallet
curl http://localhost:5220/solana/wallet/generate

# Restore a wallet
curl -X POST http://localhost:5220/solana/wallet/restore \
  -H "Content-Type: application/json" \
  -d '{"mnemonicPhrase": "your twelve word mnemonic phrase here"}'
```

Or visit the Swagger UI at http://localhost:5220/swagger when running in development mode.

## Security Notes

- These endpoints are for demonstration purposes only
- Never use generated wallets or mnemonic phrases in production without proper security measures
- Always protect private keys and mnemonic phrases
- The Solnet packages used (6.1.0) have been verified to have no known security vulnerabilities
