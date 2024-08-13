// <auto-generated />
using System;
using ASC.Migrations.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASC.Migrations.MySql.SaaS.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20240813112045_IdentityContextMigrate")]
    partial class IdentityContextMigrate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityAuthorization", b =>
                {
                    b.Property<string>("PrincipalId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("principal_id");

                    b.Property<string>("RegisteredClientId")
                        .HasColumnType("varchar(36)")
                        .HasColumnName("registered_client_id");

                    b.Property<DateTime?>("AccessTokenExpiresAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("access_token_expires_at");

                    b.Property<string>("AccessTokenHash")
                        .HasColumnType("text")
                        .HasColumnName("access_token_hash");

                    b.Property<DateTime?>("AccessTokenIssuedAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("access_token_issued_at");

                    b.Property<string>("AccessTokenMetadata")
                        .HasColumnType("text")
                        .HasColumnName("access_token_metadata");

                    b.Property<string>("AccessTokenScopes")
                        .HasColumnType("text")
                        .HasColumnName("access_token_scopes");

                    b.Property<string>("AccessTokenType")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("access_token_type");

                    b.Property<string>("AccessTokenValue")
                        .HasColumnType("text")
                        .HasColumnName("access_token_value");

                    b.Property<string>("Attributes")
                        .HasColumnType("text")
                        .HasColumnName("attributes");

                    b.Property<DateTime?>("AuthorizationCodeExpiresAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("authorization_code_expires_at");

                    b.Property<DateTime?>("AuthorizationCodeIssuedAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("authorization_code_issued_at");

                    b.Property<string>("AuthorizationCodeMetadata")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("authorization_code_metadata");

                    b.Property<string>("AuthorizationCodeValue")
                        .HasColumnType("text")
                        .HasColumnName("authorization_code_value");

                    b.Property<string>("AuthorizationGrantType")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("authorization_grant_type");

                    b.Property<string>("AuthorizedScopes")
                        .HasColumnType("text")
                        .HasColumnName("authorized_scopes");

                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<bool?>("IsInvalidated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_invalidated")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_at");

                    b.Property<DateTime?>("RefreshTokenExpiresAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("refresh_token_expires_at");

                    b.Property<string>("RefreshTokenHash")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token_hash");

                    b.Property<DateTime?>("RefreshTokenIssuedAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("refresh_token_issued_at");

                    b.Property<string>("RefreshTokenMetadata")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token_metadata");

                    b.Property<string>("RefreshTokenValue")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token_value");

                    b.Property<string>("State")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("state");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.HasKey("PrincipalId", "RegisteredClientId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "UK_id")
                        .IsUnique();

                    b.HasIndex(new[] { "IsInvalidated" }, "idx_identity_authorizations_is_invalidated");

                    b.HasIndex(new[] { "PrincipalId" }, "idx_identity_authorizations_principal_id");

                    b.HasIndex(new[] { "RegisteredClientId" }, "idx_identity_authorizations_registered_client_id");

                    b.ToTable("identity_authorizations", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityCert", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<sbyte>("PairType")
                        .HasColumnType("tinyint")
                        .HasColumnName("pair_type");

                    b.Property<string>("PrivateKey")
                        .HasColumnType("text")
                        .HasColumnName("private_key");

                    b.Property<string>("PublicKey")
                        .HasColumnType("text")
                        .HasColumnName("public_key");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("identity_certs", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClient", b =>
                {
                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("client_id");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("client_secret");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<bool?>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_enabled")
                        .HasDefaultValueSql("'1'");

                    b.Property<bool?>("IsInvalidated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_invalidated")
                        .HasDefaultValueSql("'0'");

                    b.Property<bool?>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_public")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Logo")
                        .HasColumnType("longtext")
                        .HasColumnName("logo");

                    b.Property<string>("LogoutRedirectUri")
                        .HasColumnType("tinytext")
                        .HasColumnName("logout_redirect_uri");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("PolicyUrl")
                        .HasColumnType("tinytext")
                        .HasColumnName("policy_url");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("TermsUrl")
                        .HasColumnType("tinytext")
                        .HasColumnName("terms_url");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("tinytext")
                        .HasColumnName("website_url");

                    b.HasKey("ClientId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ClientId" }, "UK_client_id")
                        .IsUnique();

                    b.HasIndex(new[] { "ClientSecret" }, "UK_client_secret")
                        .IsUnique();

                    b.HasIndex(new[] { "IsInvalidated" }, "idx_identity_clients_is_invalidated");

                    b.HasIndex(new[] { "TenantId" }, "idx_identity_clients_tenant_id");

                    b.ToTable("identity_clients", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientAllowedOrigin", b =>
                {
                    b.Property<string>("AllowedOrigin")
                        .HasColumnType("tinytext")
                        .HasColumnName("allowed_origin");

                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("client_id");

                    b.HasIndex(new[] { "ClientId" }, "idx_identity_client_allowed_origins_client_id");

                    b.ToTable("identity_client_allowed_origins", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientAuthenticationMethod", b =>
                {
                    b.Property<string>("AuthenticationMethod")
                        .HasColumnType("enum('client_secret_post','none')")
                        .HasColumnName("authentication_method");

                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("client_id");

                    b.HasIndex(new[] { "ClientId" }, "idx_client_authentication_methods_client_id");

                    b.ToTable("identity_client_authentication_methods", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientRedirectUri", b =>
                {
                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("client_id");

                    b.Property<string>("RedirectUri")
                        .HasColumnType("tinytext")
                        .HasColumnName("redirect_uri");

                    b.HasIndex(new[] { "ClientId" }, "idx_identity_client_redirect_uris_client_id");

                    b.ToTable("identity_client_redirect_uris", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientScope", b =>
                {
                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("client_id");

                    b.Property<string>("ScopeName")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("scope_name");

                    b.HasIndex(new[] { "ClientId" }, "idx_identity_client_scopes_client_id");

                    b.HasIndex(new[] { "ScopeName" }, "idx_identity_client_scopes_scope_name");

                    b.ToTable("identity_client_scopes", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityConsent", b =>
                {
                    b.Property<string>("PrincipalId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("principal_id");

                    b.Property<string>("RegisteredClientId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("registered_client_id");

                    b.Property<bool?>("IsInvalidated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_invalidated")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_at");

                    b.HasKey("PrincipalId", "RegisteredClientId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IsInvalidated" }, "idx_identity_consents_is_invalidated");

                    b.HasIndex(new[] { "PrincipalId" }, "idx_identity_consents_principal_id");

                    b.HasIndex(new[] { "RegisteredClientId" }, "idx_identity_consents_registered_client_id");

                    b.ToTable("identity_consents", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityConsentScope", b =>
                {
                    b.Property<string>("RegisteredClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("registered_client_id");

                    b.Property<string>("PrincipalId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("principal_id");

                    b.Property<string>("ScopeName")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("scope_name");

                    b.HasKey("RegisteredClientId", "PrincipalId", "ScopeName")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "PrincipalId" }, "idx_identity_consent_scopes_principal_id");

                    b.HasIndex(new[] { "RegisteredClientId" }, "idx_identity_consent_scopes_registered_client_id");

                    b.HasIndex(new[] { "ScopeName" }, "idx_identity_consent_scopes_scope_name");

                    b.ToTable("identity_consent_scopes", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityScope", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Group")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("group");

                    b.Property<string>("Type")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Name")
                        .HasName("PRIMARY");

                    b.ToTable("identity_scopes", (string)null);
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityAuthorization", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityClient", "RegisteredClient")
                        .WithMany("IdentityAuthorizations")
                        .HasForeignKey("RegisteredClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_authorization_client_id");

                    b.Navigation("RegisteredClient");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientAllowedOrigin", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityClient", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("identity_client_allowed_origins_ibfk_1");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientAuthenticationMethod", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityClient", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("identity_client_authentication_methods_ibfk_1");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientRedirectUri", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityClient", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("identity_client_redirect_uris_ibfk_1");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClientScope", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityClient", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("identity_client_scopes_ibfk_1");

                    b.HasOne("ASC.Migrations.Core.Identity.IdentityScope", "ScopeNameNavigation")
                        .WithMany()
                        .HasForeignKey("ScopeName")
                        .HasConstraintName("identity_client_scopes_ibfk_2");

                    b.Navigation("Client");

                    b.Navigation("ScopeNameNavigation");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityConsent", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityClient", "RegisteredClient")
                        .WithMany("IdentityConsents")
                        .HasForeignKey("RegisteredClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("identity_consents_ibfk_1");

                    b.Navigation("RegisteredClient");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityConsentScope", b =>
                {
                    b.HasOne("ASC.Migrations.Core.Identity.IdentityScope", "ScopeNameNavigation")
                        .WithMany("IdentityConsentScopes")
                        .HasForeignKey("ScopeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("identity_consent_scopes_ibfk_2");

                    b.HasOne("ASC.Migrations.Core.Identity.IdentityConsent", "Consent")
                        .WithMany("IdentityConsentScopes")
                        .HasForeignKey("RegisteredClientId", "PrincipalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("identity_consent_scopes_ibfk_1");

                    b.Navigation("Consent");

                    b.Navigation("ScopeNameNavigation");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityClient", b =>
                {
                    b.Navigation("IdentityAuthorizations");

                    b.Navigation("IdentityConsents");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityConsent", b =>
                {
                    b.Navigation("IdentityConsentScopes");
                });

            modelBuilder.Entity("ASC.Migrations.Core.Identity.IdentityScope", b =>
                {
                    b.Navigation("IdentityConsentScopes");
                });
#pragma warning restore 612, 618
        }
    }
}
