using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASC.Migrations.MySql.SaaS.Migrations
{
    /// <inheritdoc />
    public partial class MigrationContext_Upgrade22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_tenants_quotarow_tenants_tenants_tenant",
                table: "tenants_quotarow",
                column: "tenant",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tenants_tariff_tenants_tenants_tenant",
                table: "tenants_tariff",
                column: "tenant",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tenants_tariffrow_tenants_tenants_tenant",
                table: "tenants_tariffrow",
                column: "tenant",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_webhooks_config_tenants_tenants_tenant_id",
                table: "webhooks_config",
                column: "tenant_id",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_webhooks_logs_tenants_tenants_tenant_id",
                table: "webhooks_logs",
                column: "tenant_id",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_webstudio_settings_tenants_tenants_TenantID",
                table: "webstudio_settings",
                column: "TenantID",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_webstudio_uservisit_tenants_tenants_tenantid",
                table: "webstudio_uservisit",
                column: "tenantid",
                principalTable: "tenants_tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_audit_events_tenants_tenants_tenant_id",
                table: "audit_events");

            migrationBuilder.DropForeignKey(
                name: "FK_backup_backup_tenants_tenants_tenant_id",
                table: "backup_backup");

            migrationBuilder.DropForeignKey(
                name: "FK_backup_schedule_tenants_tenants_tenant_id",
                table: "backup_schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_core_acl_tenants_tenants_tenant",
                table: "core_acl");

            migrationBuilder.DropForeignKey(
                name: "FK_core_group_tenants_tenants_tenant",
                table: "core_group");

            migrationBuilder.DropForeignKey(
                name: "FK_core_settings_tenants_tenants_tenant",
                table: "core_settings");

            migrationBuilder.DropForeignKey(
                name: "FK_core_subscription_tenants_tenants_tenant",
                table: "core_subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_core_subscriptionmethod_tenants_tenants_tenant",
                table: "core_subscriptionmethod");

            migrationBuilder.DropForeignKey(
                name: "FK_core_user_tenants_tenants_tenant",
                table: "core_user");

            migrationBuilder.DropForeignKey(
                name: "FK_core_userdav_tenants_tenants_tenant_id",
                table: "core_userdav");

            migrationBuilder.DropForeignKey(
                name: "FK_core_usergroup_tenants_tenants_tenant",
                table: "core_usergroup");

            migrationBuilder.DropForeignKey(
                name: "FK_core_userphoto_tenants_tenants_tenant",
                table: "core_userphoto");

            migrationBuilder.DropForeignKey(
                name: "FK_core_usersecurity_tenants_tenants_tenant",
                table: "core_usersecurity");

            migrationBuilder.DropForeignKey(
                name: "FK_event_bus_integration_event_log_tenants_tenants_tenant_id",
                table: "event_bus_integration_event_log");

            migrationBuilder.DropForeignKey(
                name: "FK_files_bunch_objects_tenants_tenants_tenant_id",
                table: "files_bunch_objects");

            migrationBuilder.DropForeignKey(
                name: "FK_files_file_tenants_tenants_tenant_id",
                table: "files_file");

            migrationBuilder.DropForeignKey(
                name: "FK_files_folder_tenants_tenants_tenant_id",
                table: "files_folder");

            migrationBuilder.DropForeignKey(
                name: "FK_files_folder_tree_files_folder_folder_id",
                table: "files_folder_tree");

            migrationBuilder.DropForeignKey(
                name: "FK_files_link_tenants_tenants_tenant_id",
                table: "files_link");

            migrationBuilder.DropForeignKey(
                name: "FK_files_properties_tenants_tenants_tenant_id",
                table: "files_properties");

            migrationBuilder.DropForeignKey(
                name: "FK_files_security_tenants_tenants_tenant_id",
                table: "files_security");

            migrationBuilder.DropForeignKey(
                name: "FK_files_tag_tenants_tenants_tenant_id",
                table: "files_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_files_tag_link_tenants_tenants_tenant_id",
                table: "files_tag_link");

            migrationBuilder.DropForeignKey(
                name: "FK_files_thirdparty_account_tenants_tenants_tenant_id",
                table: "files_thirdparty_account");

            migrationBuilder.DropForeignKey(
                name: "FK_files_thirdparty_app_tenants_tenants_tenant_id",
                table: "files_thirdparty_app");

            migrationBuilder.DropForeignKey(
                name: "FK_files_thirdparty_id_mapping_tenants_tenants_tenant_id",
                table: "files_thirdparty_id_mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_firebase_users_tenants_tenants_tenant_id",
                table: "firebase_users");

            migrationBuilder.DropForeignKey(
                name: "FK_login_events_tenants_tenants_tenant_id",
                table: "login_events");

            migrationBuilder.DropForeignKey(
                name: "FK_notify_queue_tenants_tenants_tenant_id",
                table: "notify_queue");

            migrationBuilder.DropForeignKey(
                name: "FK_telegram_users_tenants_tenants_tenant_id",
                table: "telegram_users");

            migrationBuilder.DropForeignKey(
                name: "FK_tenants_iprestrictions_tenants_tenants_tenant",
                table: "tenants_iprestrictions");

            migrationBuilder.DropForeignKey(
                name: "FK_tenants_partners_tenants_tenants_tenant_id",
                table: "tenants_partners");

            migrationBuilder.DropForeignKey(
                name: "FK_tenants_quotarow_tenants_tenants_tenant",
                table: "tenants_quotarow");

            migrationBuilder.DropForeignKey(
                name: "FK_tenants_tariff_tenants_tenants_tenant",
                table: "tenants_tariff");

            migrationBuilder.DropForeignKey(
                name: "FK_tenants_tariffrow_tenants_tenants_tenant",
                table: "tenants_tariffrow");

            migrationBuilder.DropForeignKey(
                name: "FK_webhooks_config_tenants_tenants_tenant_id",
                table: "webhooks_config");

            migrationBuilder.DropForeignKey(
                name: "FK_webhooks_logs_tenants_tenants_tenant_id",
                table: "webhooks_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_webstudio_settings_tenants_tenants_TenantID",
                table: "webstudio_settings");

            migrationBuilder.DropForeignKey(
                name: "FK_webstudio_uservisit_tenants_tenants_tenantid",
                table: "webstudio_uservisit");
        }
    }
}
