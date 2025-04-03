using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.QueryConstants
{
    internal static class UserQuery
    {

        #region Read Queries
        internal const string SelectUserBase = @"Select * from [Users] WHERE IsDeleted=0";
        internal const string SelectUserCount = @"Select Count(*) from [Users] WHERE IsDeleted=0";

        internal const string SelectUserById = @"Select * from [Users] where Id=@id";

        internal const string SelectUserByNameOrEmail = @"SELECT * FROM Users WHERE Username = @Username OR Email= @Email";

        internal const string SelectUserByEmail = @"SELECT * FROM Users
                                                     WHERE Email= @Email";

        internal const string SelectRolebyId = @"SELECT R.RoleName From 
                                               UserRoles UR JOIN  Roles R
                                               ON UR.RoleId=R.Id
                                               WHERE UR.UserId=@UserId";

        internal const string SelectRoleIdbyName = @"Select * From [Roles] WHERE RoleName=@RoleName ";



        internal const string SelectCountryByName = @"Select CountryName from Countries Where CountryName=@CountryName ";



        #endregion


        #region Write Queries

        internal const string CreateUser = @"
INSERT INTO [dbo].[Users]
    ([Username]
    ,[Email]
    ,[PasswordHash]
    ,[FullName]
    ,[PhoneNumber]
    ,[IsActive]
    ,[CreatedDate]
    ,[UpdatedDate])
VALUES
    (@Username
    ,@Email
    ,@PasswordHash
    ,@FullName
    ,@PhoneNumber
    ,@IsActive
    ,@CreatedDate
    ,@UpdatedDate);

SELECT CAST(SCOPE_IDENTITY() AS INT);
";

        internal const string UpdateUser = @"

UPDATE [dbo].[Users]
SET 
    Username = @Username,
    Email = @Email,
    FullName = @FullName,
    PhoneNumber = @PhoneNumber,
    IsActive = @IsActive,
    UpdatedDate = @UpdatedDate
WHERE 
    Id = @Id;


";


        internal const string HardDeleteUser = @"

DELETE FROM [dbo].[UserRoles]
WHERE UserId=@Id

DELETE FROM [dbo].[Users]
WHERE Id=@Id;

";


        internal const string SoftDeleteUser = @"

UPDATE [dbo].[Users]

SET
UpdatedDate = @UpdatedDate,
IsDeleted=@IsDeleted
WHERE Id=@Id;

";

        internal const string AssignRole = @"
Insert Into UserRoles

Values(@UserId,@RoleId)


";


        internal const string CreateCountry = @"
Insert into Countries
(
CountryName,
CountryCode
)
Values
(
@CountryName,
@CountryCode

)

Select cast(Scope_Identity() as int)


";

        #endregion


    }
}
