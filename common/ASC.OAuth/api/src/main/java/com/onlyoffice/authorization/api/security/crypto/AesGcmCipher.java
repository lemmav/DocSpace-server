/**
 *
 */
package com.onlyoffice.authorization.api.security.crypto;

import com.onlyoffice.authorization.api.configuration.ApplicationConfiguration;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.slf4j.MDC;
import org.springframework.context.annotation.Profile;
import org.springframework.stereotype.Component;

import javax.crypto.Cipher;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.GCMParameterSpec;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;
import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.KeySpec;
import java.util.Arrays;
import java.util.Base64;

/**
 *
 */
@Profile(value = {"prod", "production", "p", "testing", "t", "test"})
@Component
@RequiredArgsConstructor
@Slf4j
public class AesGcmCipher implements com.onlyoffice.authorization.api.security.crypto.Cipher {
    private static final String ALGORITHM = "AES/GCM/NoPadding";
    private static final String FACTORY_INSTANCE = "PBKDF2WithHmacSHA256";
    private static final int TAG_LENGTH_BIT = 128;
    private static final int IV_LENGTH_BYTE = 12;
    private static final int SALT_LENGTH_BYTE = 16;
    private static final String ALGORITHM_TYPE = "AES";
    private static final int KEY_LENGTH = 128;
    private static final int ITERATION_COUNT = 1200; // Min 1000
    private static final Charset UTF_8 = StandardCharsets.UTF_8;
    private final ApplicationConfiguration configuration;

    private byte[] getRandomNonce(int length) {
        byte[] nonce = new byte[length];
        new SecureRandom().nextBytes(nonce);
        MDC.put("nonce", Arrays.toString(nonce));
        log.debug("=========RANDOM NONCE=========");
        MDC.clear();
        return nonce;
    }

    private SecretKey getSecretKey(String password, byte[] salt)
            throws NoSuchAlgorithmException, InvalidKeySpecException {
        MDC.put("password", password);
        log.debug("=========SECRET PASSWORD=========");
        MDC.clear();
        KeySpec spec = new PBEKeySpec(password.toCharArray(), salt, ITERATION_COUNT, KEY_LENGTH);

        SecretKeyFactory factory = SecretKeyFactory.getInstance(FACTORY_INSTANCE);
        return new SecretKeySpec(factory.generateSecret(spec).getEncoded(), ALGORITHM_TYPE);
    }

    private Cipher initCipher(int mode, SecretKey secretKey, byte[] iv)
            throws InvalidKeyException, InvalidAlgorithmParameterException,
            NoSuchPaddingException, NoSuchAlgorithmException {
        Cipher cipher = Cipher.getInstance(ALGORITHM);
        cipher.init(mode, secretKey, new GCMParameterSpec(TAG_LENGTH_BIT, iv));
        return cipher;
    }

    public String encrypt(String plainMessage) throws Exception {
        MDC.put("plain_message", plainMessage);
        log.info("Trying to encrypt plain message");
        MDC.clear();

        byte[] salt = getRandomNonce(SALT_LENGTH_BYTE);
        SecretKey secretKey = getSecretKey(configuration.getSecurity().getCipherSecret(), salt);

        byte[] iv = getRandomNonce(IV_LENGTH_BYTE);
        Cipher cipher = initCipher(Cipher.ENCRYPT_MODE, secretKey, iv);

        byte[] encryptedMessageByte = cipher.doFinal(plainMessage.getBytes(UTF_8));

        byte[] cipherByte = ByteBuffer.allocate(iv.length + salt.length + encryptedMessageByte.length)
                .put(iv)
                .put(salt)
                .put(encryptedMessageByte)
                .array();

        var encrypted = Base64.getEncoder().encodeToString(cipherByte);
        MDC.put("plain_message", plainMessage);
        MDC.put("encrypted_message", encrypted);
        log.info("Managed to encrypt plain text message");
        MDC.clear();
        return encrypted;
    }

    public String decrypt(String cipherMessage) throws Exception {
        MDC.put("cipher_message", cipherMessage);
        log.info("Trying to decrypt cipher message");
        MDC.clear();
        byte[] decodedCipherByte = Base64.getDecoder().decode(cipherMessage.getBytes(UTF_8));
        ByteBuffer byteBuffer = ByteBuffer.wrap(decodedCipherByte);

        byte[] iv = new byte[IV_LENGTH_BYTE];
        byteBuffer.get(iv);

        byte[] salt = new byte[SALT_LENGTH_BYTE];
        byteBuffer.get(salt);

        byte[] encryptedByte = new byte[byteBuffer.remaining()];
        byteBuffer.get(encryptedByte);

        SecretKey secretKey = getSecretKey(configuration.getSecurity().getCipherSecret(), salt);
        Cipher cipher = initCipher(Cipher.DECRYPT_MODE, secretKey, iv);

        byte[] decryptedMessageByte = cipher.doFinal(encryptedByte);

        var decrypted = new String(decryptedMessageByte, UTF_8);
        MDC.put("cipher_message", cipherMessage);
        MDC.put("decrypted_message", decrypted);
        log.info("Managed to decrypt cipher text");
        MDC.clear();
        return decrypted;
    }
}