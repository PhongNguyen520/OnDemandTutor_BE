using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class changebigdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKClass105998",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FKClass301112",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FKClass479452",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FKComplaint196647",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FKComplaint416082",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FKConversati818528",
                table: "Conversation_Account");

            migrationBuilder.DropForeignKey(
                name: "FKConversati872380",
                table: "Conversation_Account");

            migrationBuilder.DropForeignKey(
                name: "FKFeedback348843",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FKFeedback431721",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FKFeedback912586",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FKFindTutorF134374",
                table: "FindTutorForm");

            migrationBuilder.DropForeignKey(
                name: "FKFindTutorF727263",
                table: "FindTutorForm");

            migrationBuilder.DropForeignKey(
                name: "FKMessage125602",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FKMessage179454",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FKNotificati765224",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FKStudent718314",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FKSubject340947",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FKSubject866932",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FKSubject_Tu3188",
                table: "Subject_Tutor");

            migrationBuilder.DropForeignKey(
                name: "FKSubject_Tu675031",
                table: "Subject_Tutor");

            migrationBuilder.DropForeignKey(
                name: "FKTransactio181260",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FKTutor387597",
                table: "Tutor");

            migrationBuilder.DropForeignKey(
                name: "FKTutor_Ads572887",
                table: "Tutor_Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FKWallet115696",
                table: "Wallet");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Wallet__84D4F92E9F63180B",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Tutor_Ad__46AAC65A5EF65941",
                table: "Tutor_Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Tutor__77C70FC2A16C50DB",
                table: "Tutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Transact__55433A4B61451DBE",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SubjectG__2F88B016AD652F27",
                table: "SubjectGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Subject___9B67D374BBB4AFD2",
                table: "Subject_Tutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Subject__AC1BA388537DD7A1",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Student__32C52A79D012377F",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Notifica__20CF2E32FFBD5FC4",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Message__C87C037C2D324FDF",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Grade__54F87A37CC152DE4",
                table: "Grade");

            migrationBuilder.DropPrimaryKey(
                name: "PK__FindTuto__FB05B7BD12F4FFB4",
                table: "FindTutorForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Feedback__6A4BEDF65E258262",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Conversa__B31902CF056C16DA",
                table: "Conversation_Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Conversa__C050D897C401235F",
                table: "Conversation");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Complain__740D89AFCEF50736",
                table: "Complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Class__CB1927A0090BF352",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Wallet",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "WalletID",
                table: "Wallet",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldUnicode: false,
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Video",
                table: "Tutor_Ads",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Tutor_Ads",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Tutor_Ads",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Tutor_Ads",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "AdsID",
                table: "Tutor_Ads",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "Tutor_Ads",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tutor_Ads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfDegree",
                table: "Tutor",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "Tutor",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Headline",
                table: "Tutor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Education",
                table: "Tutor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "Tutor",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tutor",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Tutor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Tutor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "WalletID",
                table: "Transaction",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionID",
                table: "Transaction",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "SubjectGroup",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SubjectGroup",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectGroupID",
                table: "SubjectGroup",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Subject_Tutor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "Subject_Tutor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectGroupID",
                table: "Subject",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "GradeID",
                table: "Subject",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "Subject",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SchoolName",
                table: "Student",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Student",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notification",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationID",
                table: "Notification",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Message",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Message",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ConversationID",
                table: "Message",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "MessageID",
                table: "Message",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "GradeID",
                table: "Grade",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfDegree",
                table: "FindTutorForm",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "FindTutorForm",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "FindTutorForm",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "FindTutorForm",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "DescribeTutor",
                table: "FindTutorForm",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "FindTutorForm",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "FormID",
                table: "FindTutorForm",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "FindTutorForm",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MaxHourlyRate",
                table: "FindTutorForm",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinHourlyRate",
                table: "FindTutorForm",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FindTutorForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Feedback",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Feedback",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Feedback",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Feedback",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "ClassID",
                table: "Feedback",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackID",
                table: "Feedback",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Feedback",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConversationID",
                table: "Conversation_Account",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Conversation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "ConversationID",
                table: "Conversation",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Complaint",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Complaint",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Complaint",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDay",
                table: "Complaint",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "ComplaintID",
                table: "Complaint",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Class",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "Class",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Class",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Class",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Class",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ClassID",
                table: "Class",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Wallet__84D4F92E9F63180BD",
                table: "Wallet",
                column: "WalletID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Tutor_Ad__46AAC65A5EF65941D",
                table: "Tutor_Ads",
                column: "AdsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Tutor__77C70FC2A16C50DBD",
                table: "Tutor",
                column: "TutorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Transact__55433A4B61451DBED",
                table: "Transaction",
                column: "TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SubjectG__2F88B016AD652F27D",
                table: "SubjectGroup",
                column: "SubjectGroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Subject___9B67D374BBB4AFD2D",
                table: "Subject_Tutor",
                columns: new[] { "SubjectID", "TutorID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Subject__AC1BA388537DD7A1D",
                table: "Subject",
                column: "SubjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Student__32C52A79D012377FD",
                table: "Student",
                column: "StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Notifica__20CF2E32FFBD5FC4D",
                table: "Notification",
                column: "NotificationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Message__C87C037C2D324FDFD",
                table: "Message",
                column: "MessageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Grade__54F87A37CC152DE4D",
                table: "Grade",
                column: "GradeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__FindTuto__FB05B7BD12F4FFB4D",
                table: "FindTutorForm",
                column: "FormID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Feedback__6A4BEDF65E258262D",
                table: "Feedback",
                column: "FeedbackID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Conversa__B31902CF056C16DAD",
                table: "Conversation_Account",
                columns: new[] { "ConversationID", "AccountID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Conversa__C050D897C401235FD",
                table: "Conversation",
                column: "ConversationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Complain__740D89AFCEF50736D",
                table: "Complaint",
                column: "ComplaintID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Class__CB1927A0090BF352J",
                table: "Class",
                column: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FKClass1059982",
                table: "Class",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FKClass3011122",
                table: "Class",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKClass4794522",
                table: "Class",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKComplaint1966472",
                table: "Complaint",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKComplaint4160822",
                table: "Complaint",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKConversati8185282",
                table: "Conversation_Account",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKConversati8723802",
                table: "Conversation_Account",
                column: "ConversationID",
                principalTable: "Conversation",
                principalColumn: "ConversationID");

            migrationBuilder.AddForeignKey(
                name: "FKFeedback3488432",
                table: "Feedback",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKFeedback4317212",
                table: "Feedback",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKFeedback9125862",
                table: "Feedback",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FKFindTutorF1343742",
                table: "FindTutorForm",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKFindTutorF7272632",
                table: "FindTutorForm",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FKMessage1256022",
                table: "Message",
                column: "ConversationID",
                principalTable: "Conversation",
                principalColumn: "ConversationID");

            migrationBuilder.AddForeignKey(
                name: "FKMessage1794542",
                table: "Message",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKNotificati7652242",
                table: "Notification",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKStudent7183142",
                table: "Student",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKSubject3409472",
                table: "Subject",
                column: "SubjectGroupID",
                principalTable: "SubjectGroup",
                principalColumn: "SubjectGroupID");

            migrationBuilder.AddForeignKey(
                name: "FKSubject8669322",
                table: "Subject",
                column: "GradeID",
                principalTable: "Grade",
                principalColumn: "GradeID");

            migrationBuilder.AddForeignKey(
                name: "FKSubject_Tu31882",
                table: "Subject_Tutor",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKSubject_Tu6750312",
                table: "Subject_Tutor",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FKTransactio1812602",
                table: "Transaction",
                column: "WalletID",
                principalTable: "Wallet",
                principalColumn: "WalletID");

            migrationBuilder.AddForeignKey(
                name: "FKTutor3875972",
                table: "Tutor",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKTutor_Ads5728872",
                table: "Tutor_Ads",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKWallet1156962",
                table: "Wallet",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKClass1059982",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FKClass3011122",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FKClass4794522",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FKComplaint1966472",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FKComplaint4160822",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FKConversati8185282",
                table: "Conversation_Account");

            migrationBuilder.DropForeignKey(
                name: "FKConversati8723802",
                table: "Conversation_Account");

            migrationBuilder.DropForeignKey(
                name: "FKFeedback3488432",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FKFeedback4317212",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FKFeedback9125862",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FKFindTutorF1343742",
                table: "FindTutorForm");

            migrationBuilder.DropForeignKey(
                name: "FKFindTutorF7272632",
                table: "FindTutorForm");

            migrationBuilder.DropForeignKey(
                name: "FKMessage1256022",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FKMessage1794542",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FKNotificati7652242",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FKStudent7183142",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FKSubject3409472",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FKSubject8669322",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FKSubject_Tu31882",
                table: "Subject_Tutor");

            migrationBuilder.DropForeignKey(
                name: "FKSubject_Tu6750312",
                table: "Subject_Tutor");

            migrationBuilder.DropForeignKey(
                name: "FKTransactio1812602",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FKTutor3875972",
                table: "Tutor");

            migrationBuilder.DropForeignKey(
                name: "FKTutor_Ads5728872",
                table: "Tutor_Ads");

            migrationBuilder.DropForeignKey(
                name: "FKWallet1156962",
                table: "Wallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Wallet__84D4F92E9F63180BD",
                table: "Wallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Tutor_Ad__46AAC65A5EF65941D",
                table: "Tutor_Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Tutor__77C70FC2A16C50DBD",
                table: "Tutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Transact__55433A4B61451DBED",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SubjectG__2F88B016AD652F27D",
                table: "SubjectGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Subject___9B67D374BBB4AFD2D",
                table: "Subject_Tutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Subject__AC1BA388537DD7A1D",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Student__32C52A79D012377FD",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Notifica__20CF2E32FFBD5FC4D",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Message__C87C037C2D324FDFD",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Grade__54F87A37CC152DE4D",
                table: "Grade");

            migrationBuilder.DropPrimaryKey(
                name: "PK__FindTuto__FB05B7BD12F4FFB4D",
                table: "FindTutorForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Feedback__6A4BEDF65E258262D",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Conversa__B31902CF056C16DAD",
                table: "Conversation_Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Conversa__C050D897C401235FD",
                table: "Conversation");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Complain__740D89AFCEF50736D",
                table: "Complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Class__CB1927A0090BF352J",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "Tutor_Ads");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tutor_Ads");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "MaxHourlyRate",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "MinHourlyRate",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Feedback");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Wallet",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Wallet",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "WalletID",
                table: "Wallet",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Users",
                type: "varchar(5)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Video",
                table: "Tutor_Ads",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Tutor_Ads",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Tutor_Ads",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Tutor_Ads",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AdsID",
                table: "Tutor_Ads",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfDegree",
                table: "Tutor",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "Tutor",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Headline",
                table: "Tutor",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Education",
                table: "Tutor",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Dob",
                table: "Tutor",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tutor",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Tutor",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Tutor",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "WalletID",
                table: "Transaction",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Transaction",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionID",
                table: "Transaction",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "SubjectGroup",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SubjectGroup",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectGroupID",
                table: "SubjectGroup",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Subject_Tutor",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "Subject_Tutor",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectGroupID",
                table: "Subject",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "GradeID",
                table: "Subject",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "Subject",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SchoolName",
                table: "Student",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Student",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notification",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Notification",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationID",
                table: "Notification",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Time",
                table: "Message",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Message",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ConversationID",
                table: "Message",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "MessageID",
                table: "Message",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "GradeID",
                table: "Grade",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfDegree",
                table: "FindTutorForm",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "FindTutorForm",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "FindTutorForm",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "FindTutorForm",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "DescribeTutor",
                table: "FindTutorForm",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "FindTutorForm",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "FormID",
                table: "FindTutorForm",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Feedback",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Feedback",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Feedback",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Feedback",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ClassID",
                table: "Feedback",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackID",
                table: "Feedback",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ConversationID",
                table: "Conversation_Account",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Conversation",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ConversationID",
                table: "Conversation",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Complaint",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Complaint",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Complaint",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreateDay",
                table: "Complaint",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ComplaintID",
                table: "Complaint",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TutorID",
                table: "Class",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectID",
                table: "Class",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "Class",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Class",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Class",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ClassID",
                table: "Class",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Wallet__84D4F92E9F63180B",
                table: "Wallet",
                column: "WalletID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Tutor_Ad__46AAC65A5EF65941",
                table: "Tutor_Ads",
                column: "AdsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Tutor__77C70FC2A16C50DB",
                table: "Tutor",
                column: "TutorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Transact__55433A4B61451DBE",
                table: "Transaction",
                column: "TransactionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SubjectG__2F88B016AD652F27",
                table: "SubjectGroup",
                column: "SubjectGroupID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Subject___9B67D374BBB4AFD2",
                table: "Subject_Tutor",
                columns: new[] { "SubjectID", "TutorID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Subject__AC1BA388537DD7A1",
                table: "Subject",
                column: "SubjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Student__32C52A79D012377F",
                table: "Student",
                column: "StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Notifica__20CF2E32FFBD5FC4",
                table: "Notification",
                column: "NotificationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Message__C87C037C2D324FDF",
                table: "Message",
                column: "MessageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Grade__54F87A37CC152DE4",
                table: "Grade",
                column: "GradeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__FindTuto__FB05B7BD12F4FFB4",
                table: "FindTutorForm",
                column: "FormID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Feedback__6A4BEDF65E258262",
                table: "Feedback",
                column: "FeedbackID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Conversa__B31902CF056C16DA",
                table: "Conversation_Account",
                columns: new[] { "ConversationID", "AccountID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Conversa__C050D897C401235F",
                table: "Conversation",
                column: "ConversationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Complain__740D89AFCEF50736",
                table: "Complaint",
                column: "ComplaintID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Class__CB1927A0090BF352",
                table: "Class",
                column: "ClassID");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE3A42B64DA9", x => x.RoleID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FKClass105998",
                table: "Class",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FKClass301112",
                table: "Class",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKClass479452",
                table: "Class",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKComplaint196647",
                table: "Complaint",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKComplaint416082",
                table: "Complaint",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKConversati818528",
                table: "Conversation_Account",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKConversati872380",
                table: "Conversation_Account",
                column: "ConversationID",
                principalTable: "Conversation",
                principalColumn: "ConversationID");

            migrationBuilder.AddForeignKey(
                name: "FKFeedback348843",
                table: "Feedback",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKFeedback431721",
                table: "Feedback",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKFeedback912586",
                table: "Feedback",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FKFindTutorF134374",
                table: "FindTutorForm",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FKFindTutorF727263",
                table: "FindTutorForm",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FKMessage125602",
                table: "Message",
                column: "ConversationID",
                principalTable: "Conversation",
                principalColumn: "ConversationID");

            migrationBuilder.AddForeignKey(
                name: "FKMessage179454",
                table: "Message",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKNotificati765224",
                table: "Notification",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKStudent718314",
                table: "Student",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKSubject340947",
                table: "Subject",
                column: "SubjectGroupID",
                principalTable: "SubjectGroup",
                principalColumn: "SubjectGroupID");

            migrationBuilder.AddForeignKey(
                name: "FKSubject866932",
                table: "Subject",
                column: "GradeID",
                principalTable: "Grade",
                principalColumn: "GradeID");

            migrationBuilder.AddForeignKey(
                name: "FKSubject_Tu3188",
                table: "Subject_Tutor",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FKSubject_Tu675031",
                table: "Subject_Tutor",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FKTransactio181260",
                table: "Transaction",
                column: "WalletID",
                principalTable: "Wallet",
                principalColumn: "WalletID");

            migrationBuilder.AddForeignKey(
                name: "FKTutor387597",
                table: "Tutor",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKTutor_Ads572887",
                table: "Tutor_Ads",
                column: "TutorID",
                principalTable: "Tutor",
                principalColumn: "TutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FKWallet115696",
                table: "Wallet",
                column: "AccountID",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
