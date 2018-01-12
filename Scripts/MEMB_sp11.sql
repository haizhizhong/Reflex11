if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBA_ConditionText]') and xtype = 'FN')  
 drop Function DBA_ConditionText
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

create function [dbo].[DBA_ConditionText](@Routine varchar(max), @xType varchar(20))returns varchar(max) as
begin
	--declare @Routine varchar(max), @xType varchar(20)
	--select @Routine = '[dbo].[usp_IN_AccountSubCodeGetByAccountNumber]', @xType = 'P'
	
	/*
	Author:		Robg
	Purpose:	Condition the routine to follow standard header formation
	*/
	declare @Subject varchar(20)
	select @Subject = case @xtype	when 'TR' then 'Trigger'
									when 'P' then 'Proc'
									when 'FN' then 'Func'
									when 'V' then 'View' end

	----strip out leading charaters
	--select @Routine = replace(@Routine, substring(@routine, 1, charindex('create', @Routine)-1), '')
	--where charindex('create', @Routine)-1 > 0
	
	
	--strip out spaces & tabs
	declare @x int select @x = 2
	while @x < 50 and substring(@routine, charindex('create', @Routine)-1, 7) + @subject != 'create ' + @subject
	begin
		select @Routine = replace(@Routine, 'create' + replicate(' ', @x) + @subject, 'create ' + @subject)
		select @Routine = replace(@Routine, 'create' + replicate(char(9), @x) + @subject,  'create ' + @subject)
		select @x = @x + 1	
	end
		
	return @Routine
	--select @Routine
end


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_AnyDataInTables]') and xtype = 'P ')  
 drop Procedure aspnet_AnyDataInTables
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE [dbo].aspnet_AnyDataInTables
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'aspnet_WebEvent_LogEvent') AND (type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Applications_CreateApplication]') and xtype = 'P ')  
 drop Procedure aspnet_Applications_CreateApplication
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_Applications_CreateApplication
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_CheckSchemaVersion]') and xtype = 'P ')  
 drop Procedure aspnet_CheckSchemaVersion
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_CheckSchemaVersion
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_ChangePasswordQuestionAndAnswer
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_ChangePasswordQuestionAndAnswer
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_CreateUser]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_CreateUser
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_CreateUser
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_FindUsersByEmail]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_FindUsersByEmail
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_FindUsersByEmail
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_FindUsersByName]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_FindUsersByName
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_FindUsersByName
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetAllUsers]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetAllUsers
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetAllUsers
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetNumberOfUsersOnline]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetNumberOfUsersOnline
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetNumberOfUsersOnline
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetPassword]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetPassword
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetPassword
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetPasswordWithFormat]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetPasswordWithFormat
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetPasswordWithFormat
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetUserByEmail]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetUserByEmail
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetUserByEmail
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetUserByName]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetUserByName
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetUserByName
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  @UserId = u.UserId AND u.UserId = m.UserId 
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_GetUserByUserId]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_GetUserByUserId
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_GetUserByUserId
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_ResetPassword]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_ResetPassword
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_ResetPassword
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_SetPassword]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_SetPassword
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_SetPassword
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_UnlockUser]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_UnlockUser
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_UnlockUser
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_UpdateUser]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_UpdateUser
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_UpdateUser
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Membership_UpdateUserInfo]') and xtype = 'P ')  
 drop Procedure aspnet_Membership_UpdateUserInfo
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Membership_UpdateUserInfo
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
  FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Paths_CreatePath]') and xtype = 'P ')  
 drop Procedure aspnet_Paths_CreatePath
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Paths_CreatePath
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Personalization_GetApplicationId]') and xtype = 'P ')  
 drop Procedure aspnet_Personalization_GetApplicationId
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Personalization_GetApplicationId (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAdministration_DeleteAllState]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAdministration_DeleteAllState
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAdministration_DeleteAllState (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAdministration_FindState]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAdministration_FindState
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAdministration_FindState (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
    AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAdministration_GetCountOfState]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAdministration_GetCountOfState
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAdministration_GetCountOfState (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAdministration_ResetSharedState]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAdministration_ResetSharedState
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAdministration_ResetSharedState (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAdministration_ResetUserState]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAdministration_ResetUserState
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAdministration_ResetUserState (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAllUsers_GetPageSettings
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAllUsers_GetPageSettings (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAllUsers_ResetPageSettings
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAllUsers_ResetPageSettings (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationAllUsers_SetPageSettings
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationAllUsers_SetPageSettings (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationPerUser_GetPageSettings]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationPerUser_GetPageSettings
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationPerUser_GetPageSettings (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationPerUser_ResetPageSettings
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationPerUser_ResetPageSettings (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_PersonalizationPerUser_SetPageSettings]') and xtype = 'P ')  
 drop Procedure aspnet_PersonalizationPerUser_SetPageSettings
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_PersonalizationPerUser_SetPageSettings (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Profile_DeleteInactiveProfiles]') and xtype = 'P ')  
 drop Procedure aspnet_Profile_DeleteInactiveProfiles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Profile_DeleteInactiveProfiles
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Profile_DeleteProfiles]') and xtype = 'P ')  
 drop Procedure aspnet_Profile_DeleteProfiles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Profile_DeleteProfiles
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N',', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]') and xtype = 'P ')  
 drop Procedure aspnet_Profile_GetNumberOfInactiveProfiles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Profile_GetNumberOfInactiveProfiles
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Profile_GetProfiles]') and xtype = 'P ')  
 drop Procedure aspnet_Profile_GetProfiles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Profile_GetProfiles
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Profile_GetProperties]') and xtype = 'P ')  
 drop Procedure aspnet_Profile_GetProperties
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Profile_GetProperties
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Profile_SetProperties]') and xtype = 'P ')  
 drop Procedure aspnet_Profile_SetProperties
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Profile_SetProperties
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_RegisterSchemaVersion]') and xtype = 'P ')  
 drop Procedure aspnet_RegisterSchemaVersion
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_RegisterSchemaVersion
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Roles_CreateRole]') and xtype = 'P ')  
 drop Procedure aspnet_Roles_CreateRole
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_Roles_CreateRole
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Roles_DeleteRole]') and xtype = 'P ')  
 drop Procedure aspnet_Roles_DeleteRole
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Roles_DeleteRole
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Roles_GetAllRoles]') and xtype = 'P ')  
 drop Procedure aspnet_Roles_GetAllRoles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Roles_GetAllRoles (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Roles_RoleExists]') and xtype = 'P ')  
 drop Procedure aspnet_Roles_RoleExists
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_Roles_RoleExists
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Setup_RemoveAllRoleMembers]') and xtype = 'P ')  
 drop Procedure aspnet_Setup_RemoveAllRoleMembers
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_Setup_RemoveAllRoleMembers
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Setup_RestorePermissions]') and xtype = 'P ')  
 drop Procedure aspnet_Setup_RestorePermissions
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_Setup_RestorePermissions
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UnRegisterSchemaVersion]') and xtype = 'P ')  
 drop Procedure aspnet_UnRegisterSchemaVersion
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_UnRegisterSchemaVersion
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Users_CreateUser]') and xtype = 'P ')  
 drop Procedure aspnet_Users_CreateUser
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE [dbo].aspnet_Users_CreateUser
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_Users_DeleteUser]') and xtype = 'P ')  
 drop Procedure aspnet_Users_DeleteUser
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE [dbo].aspnet_Users_DeleteUser
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UsersInRoles_AddUsersToRoles]') and xtype = 'P ')  
 drop Procedure aspnet_UsersInRoles_AddUsersToRoles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_UsersInRoles_AddUsersToRoles
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UsersInRoles_FindUsersInRole]') and xtype = 'P ')  
 drop Procedure aspnet_UsersInRoles_FindUsersInRole
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_UsersInRoles_FindUsersInRole
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UsersInRoles_GetRolesForUser]') and xtype = 'P ')  
 drop Procedure aspnet_UsersInRoles_GetRolesForUser
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_UsersInRoles_GetRolesForUser
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UsersInRoles_GetUsersInRoles]') and xtype = 'P ')  
 drop Procedure aspnet_UsersInRoles_GetUsersInRoles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_UsersInRoles_GetUsersInRoles
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UsersInRoles_IsUserInRole]') and xtype = 'P ')  
 drop Procedure aspnet_UsersInRoles_IsUserInRole
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_UsersInRoles_IsUserInRole
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]') and xtype = 'P ')  
 drop Procedure aspnet_UsersInRoles_RemoveUsersFromRoles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE PROCEDURE dbo.aspnet_UsersInRoles_RemoveUsersFromRoles
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[aspnet_WebEvent_LogEvent]') and xtype = 'P ')  
 drop Procedure aspnet_WebEvent_LogEvent
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.aspnet_WebEvent_LogEvent
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBA_IndexDiscovery]') and xtype = 'P ')  
 drop Procedure DBA_IndexDiscovery
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc DBA_IndexDiscovery as

declare @Version int select @Version = case when substring (@@Version, 1, 25) = 'Microsoft SQL Server 2008' then 2008 else 2005 end

declare @Source varchar(100) select @Source = 'Unknown'
SET NOCOUNT ON


if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#massaged_tmp_indexes'))
drop table #massaged_tmp_indexes
CREATE TABLE #massaged_tmp_indexes(
	TableID int,
	TableName varchar(max),
	IndexID int,
	IndexName varchar(max),
	IndexDefinition varchar(max),
	CurrentState varchar(50) not null default 'Implemented')

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#tmp_indexes'))
drop table #tmp_indexes
create table #tmp_indexes(
	object_id int,
	tablename varchar(max),
	tableid int,
	indexid int,
	indexname varchar(max),
	isunique bit,
	isclustered bit,
	indexfillfactor int,
	Filter_Definition varchar(max),
	keycolumns VARCHAR(max), 
	includes VARCHAR(max))

if not exists (select * from sys.tables t where t.name = 'DBA_Reflex_Index_Storage')
begin
	select @Source = 'Reflex Setup'
	create table DBA_Reflex_Index_Storage(
		id int not null identity(1,1) primary key,
		TableID int,
		TableName varchar(max),
		IndexID int,
		IndexName varchar(max),
		IndexDefinition varchar(max),
		CurrentState varchar(50) not null default 'Implemented',
		IndexSource varchar(100) not null default 'Unknown',
		DropIndex bit not null default 0,
		ResurectIndex bit not null default 0)
end

insert #tmp_indexes (object_id, tablename, tableid, indexid, indexname, isunique, isclustered, indexfillfactor, Filter_Definition )
SELECT
ixz.object_id,
tablename = QUOTENAME(scmz.name) + '.' + QUOTENAME((OBJECT_NAME(ixz.object_id))),
tableid = ixz.object_id,
indexid = ixz.index_id,
indexname = ixz.name,
isunique = INDEXPROPERTY (ixz.object_id,ixz.name,'isunique'),
isclustered = INDEXPROPERTY (ixz.object_id,ixz.name,'isclustered'),
indexfillfactor = INDEXPROPERTY (ixz.object_id,ixz.name,'indexfillfactor'),
case when @Version = 2008 then
	--SQL2008+ Filtered indexes:
	CASE
	WHEN ixz.filter_definition IS NULL
	THEN ''
	ELSE ' WHERE ' + ixz.filter_definition
	END 
else 
'' 
end 
Filter_Definition
FROM sys.indexes ixz
INNER JOIN sys.objects obz ON ixz.object_id = obz.object_id
INNER JOIN sys.schemas scmz ON obz.schema_id = scmz.schema_id
WHERE ixz.index_id > 0
AND ixz.index_id < 255 ---- 0 = HEAP index, 255 = TEXT columns index
AND INDEXPROPERTY (ixz.object_id,ixz.name,'ISUNIQUE') = 0 -- comment out to include unique and
AND INDEXPROPERTY (ixz.object_id,ixz.name,'ISCLUSTERED') = 0 -- comment out to include PK's


DECLARE @isql_key VARCHAR(max), @isql_incl VARCHAR(max), @tableid INT, @indexid INT

DECLARE index_cursor CURSOR
FOR
SELECT tableid, indexid FROM #tmp_indexes
OPEN index_cursor
FETCH NEXT FROM index_cursor INTO @tableid, @indexid

WHILE @@FETCH_STATUS <> -1
BEGIN
	SELECT @isql_key = '', @isql_incl = ''
	SELECT --ixz.name, colz.colid, colz.name, ixcolz.index_id, ixcolz.object_id, *
	--key column
	@isql_key = 
		CASE ixcolz.is_included_column
		WHEN 0 THEN 
			CASE ixcolz.is_descending_key
			WHEN 1 THEN @isql_key + COALESCE(colz.name,'') + ' DESC, '
			ELSE @isql_key + COALESCE(colz.name,'') + ' ASC, '
			END
		ELSE @isql_key END,

	--include column
	@isql_incl = 
		CASE ixcolz.is_included_column
		WHEN 1 THEN 
			CASE ixcolz.is_descending_key
			WHEN 1 THEN @isql_incl + COALESCE(colz.name,'') + ', '
			ELSE @isql_incl + COALESCE(colz.name,'') + ', ' END
		ELSE @isql_incl END
	FROM sysindexes ixz
	INNER JOIN sys.index_columns AS ixcolz ON (ixcolz.column_id > 0 AND ( ixcolz.key_ordinal > 0 OR ixcolz.partition_ordinal = 0 OR ixcolz.is_included_column != 0)	)
	AND ( ixcolz.index_id=CAST(ixz.indid AS INT) AND ixcolz.object_id=ixz.id )
	INNER JOIN sys.columns AS colz ON colz.object_id = ixcolz.object_id AND colz.column_id = ixcolz.column_id
	WHERE ixz.indid > 0 AND ixz.indid < 255
	AND (ixz.status & 64) = 0
	AND ixz.id = @tableid
	AND ixz.indid = @indexid
	ORDER BY ixz.name, 
		CASE ixcolz.is_included_column
		WHEN 1 THEN ixcolz.index_column_id
		ELSE ixcolz.key_ordinal
		END

	--remove any trailing commas from the cursor results
	IF LEN(@isql_key) > 1 SET @isql_key = LEFT(@isql_key, LEN(@isql_key) -1)
	IF LEN(@isql_incl) > 1 SET @isql_incl = LEFT(@isql_incl, LEN(@isql_incl) -1)
	--put the columns collection into our temp table
	UPDATE #tmp_indexes SET keycolumns = @isql_key, includes = @isql_incl
	WHERE tableid = @tableid AND indexid = @indexid
FETCH NEXT FROM index_cursor INTO @tableid,@indexid
END --WHILE
CLOSE index_cursor
DEALLOCATE index_cursor

--remove invalid indexes, ie ones without key columns
DELETE FROM #tmp_indexes WHERE keycolumns = ''

insert #massaged_tmp_indexes
SELECT ti.tableid, ti.TABLENAME, ti.indexid, ti.INDEXNAME, 
'IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''' + ti.TABLENAME + '''' + ') AND name = N' + '''' + ti.INDEXNAME + '''' + ')' + ' ' +
'CREATE '
+ CASE WHEN ti.ISUNIQUE = 1 THEN 'UNIQUE ' ELSE '' END
+ CASE WHEN ti.ISCLUSTERED = 1 THEN 'CLUSTERED ' ELSE '' END
+ 'INDEX ' + QUOTENAME(ti.INDEXNAME)
+ ' ON ' + (ti.TABLENAME) + ' '
+ '(' + ti.keycolumns + ')'
+ CASE
WHEN ti.INDEXFILLFACTOR = 0 AND ti.ISCLUSTERED = 1 AND INCLUDES = '' THEN ti.Filter_Definition + ' WITH (SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
WHEN INDEXFILLFACTOR = 0 AND ti.ISCLUSTERED = 0 AND ti.INCLUDES = '' THEN ti.Filter_Definition + ' WITH (ONLINE = ON, SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
WHEN INDEXFILLFACTOR <> 0 AND ti.ISCLUSTERED = 0 AND ti.INCLUDES = '' THEN ti.Filter_Definition + ' WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = ' + CONVERT(VARCHAR(10),ti.INDEXFILLFACTOR) + ') ON [' + fg.name + ']'
WHEN INDEXFILLFACTOR = 0 AND ti.ISCLUSTERED = 0 AND ti.INCLUDES <> '' THEN ' INCLUDE (' + ti.INCLUDES + ') ' + ti.Filter_Definition + ' WITH (ONLINE = ON, SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
ELSE ' INCLUDE(' + ti.INCLUDES + ') ' + ti.Filter_Definition + ' WITH (FILLFACTOR = ' + CONVERT(VARCHAR(10),ti.INDEXFILLFACTOR) + ', ONLINE = ON, SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
END [IndexDefinition], 'Implemented' CurrentState
FROM #tmp_indexes ti
JOIN sys.indexes i ON ti.Object_id = i.object_id and ti.indexname = i.name 
JOIN sys.filegroups fg on i.data_space_id = fg.data_space_id
WHERE LEFT(ti.tablename,3) NOT IN ('sys', 'dt_') --exclude system tables
ORDER BY ti.tablename, ti.indexid, ti.indexname

insert DBA_Reflex_Index_Storage(TableID, TableName, IndexID, IndexName, IndexDefinition, CurrentState, IndexSource)
select TableID, TableName, IndexID, IndexName, IndexDefinition, CurrentState, @Source
from #massaged_tmp_indexes
where IndexDefinition not in (select IndexDefinition from DBA_Reflex_Index_Storage)



--SET NOCOUNT OFF


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DEF_DropAllTableConstraints]') and xtype = 'P ')  
 drop Procedure DEF_DropAllTableConstraints
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE procedure [dbo].[DEF_DropAllTableConstraints] @table varchar(255) as
--declare @table varchar(255) select @table = 'customers'
/*
!!!this procedure is used within the company copy function for the import staging database only
	Robg... this will drop all constraints on the @table
	be a bad idea to run on a production database (see me for details)
*/
declare @nr int, @c varchar(255), @t varchar(255), @sql nvarchar(1000)

DECLARE c1 CURSOR FOR 
select 2,CONSTRAINT_NAME,table_name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
where table_name = @table and CONSTRAINT_TYPE != 'PRIMARY KEY'
union
select 1,k.Name,s2.name from sys.foreign_keys k
inner join dbo.sysobjects s on s.id = k.referenced_object_id 
inner join dbo.sysobjects s2 on s2.id = k.[parent_object_id]
where s.name=@table
order by 1

OPEN c1
FETCH NEXT FROM c1 INTO @nr, @c ,@t
WHILE @@FETCH_STATUS = 0
BEGIN
	select @sql = 'ALTER TABLE ' + @t + ' DROP CONSTRAINT ' + @c
	print @sql
	exec sp_executesql @sql
	FETCH NEXT FROM c1 INTO @nr, @c, @t 
END
CLOSE c1
DEALLOCATE c1



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DEF_DropAllTableConstraintsAndPK]') and xtype = 'P ')  
 drop Procedure DEF_DropAllTableConstraintsAndPK
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure [dbo].[DEF_DropAllTableConstraintsAndPK] @table varchar(255) as
--declare @table varchar(255) select @table = 'customers'
/*
!!!this procedure is used within the company copy function for the import staging database only
	Robg... this will drop all constraints on the @table
	be a bad idea to run on a production database (see me for details)
*/
declare @nr int, @c varchar(255), @t varchar(255), @sql nvarchar(1000)

DECLARE c1 CURSOR FOR 
select 2,CONSTRAINT_NAME,table_name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
where table_name = @table 
union
select 1,k.Name,s2.name from sys.foreign_keys k
inner join dbo.sysobjects s on s.id = k.referenced_object_id 
inner join dbo.sysobjects s2 on s2.id = k.[parent_object_id]
where s.name=@table
order by 1

OPEN c1
FETCH NEXT FROM c1 INTO @nr, @c ,@t
WHILE @@FETCH_STATUS = 0
BEGIN
	select @sql = 'ALTER TABLE ' + @t + ' DROP CONSTRAINT ' + @c
	print @sql
	exec sp_executesql @sql
	FETCH NEXT FROM c1 INTO @nr, @c, @t 
END
CLOSE c1
DEALLOCATE c1



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_GenerateScripts]') and xtype = 'P ')  
 drop Procedure sp_GenerateScripts
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE Procedure [dbo].[sp_GenerateScripts] @ScriptBuildType varchar(1) = 'D' AS
/*
@ScriptBuildType = 'D'(rop and create) is fast
@ScriptBuildType = 'A'(lter) is slow
@ScriptBuildType = 'C'(reate only) is fast
*/
--declare @ScriptBuildType varchar(1) select @ScriptBuildType = 'A'
set nocount on

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__Scripts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table __Scripts
create table __Scripts(id int identity(1,1) not null, SQL varchar(8000) null, xType varchar(10))

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__ScriptProgress]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)	
create table __ScriptProgress(id int identity(1,1) not null, ScriptObject varchar(8000) null, oType varchar(25), xCount int, tCount int)
truncate table __ScriptProgress

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#SQL'))
drop table #SQL
create table #SQL(SQL varchar(8000), id int not null identity(1,1))

declare @Name varchar(500), @XType varchar(10), @SQL varchar(8000), @NewLine char(2), @xCount int, @tCount int

set @NewLine=char(13)+char(10)

select @tCount = count(*), @xCount = 0
from sysobjects
where (xtype = 'P' or xtype = 'FN' or xtype = 'V' or xtype='TR') and status >= 0 
--and name = 'usp_IN_AccountSubCodeGetByAccountNumber'

insert __ScriptProgress(ScriptObject, oType, xCount, tCount)
select 'Initializing', 'N/A', 0, @tCount

declare PrincCursor cursor
read_only
for
	 select name, xtype
	 from sysobjects
	 where (xtype = 'P' or xtype = 'FN' or xtype = 'V' or xtype='TR') and status >= 0 
--	and name = 'usp_IN_AccountSubCodeGetByAccountNumber'
	 order by xtype, name
	 
open PrincCursor
fetch next from PrincCursor into @name, @xtype
while (@@fetch_status <> -1)
begin
	 if (@@fetch_status <> -2)
	 begin  			
		truncate table #SQL  
		--quick build, does drop / create 
		if @ScriptBuildType = 'D'
		begin
			select @SQL = 'if exists (select * from dbo.sysobjects where id = object_id(N''[dbo].[' + @Name + ']'') and xtype = ''' + @xtype + ''')  ' +  char(13) + char(10) + ' drop '
			if @xtype = 'TR' 
				select @SQL = @SQL + 'Trigger ' + @Name
			if @xtype = 'P' 
				select @SQL = @SQL + 'Procedure ' + @Name
			if @xtype = 'FN' 
				select @SQL = @SQL + 'Function ' + @Name
			if @xtype = 'V' 
				select @SQL = @SQL + 'View ' + @Name 
			insert #SQL select @SQL 
		end

		--find the first line and update to get alter
		declare @Subject varchar(20)
		select @Subject = case @xtype when 'TR' then 'Trigger'
								when 'P' then 'Proc'
								when 'FN' then 'Function'
								when 'V' then 'View' end
		
		update __ScriptProgress set ScriptObject = @name, oType = @Subject, xCount = @xCount
		select @xCount = @xCount + 1
					
		insert #SQL select char(13) + char(10) + 'Go' + char(13) + char(10)
		if @Name in ('fn_GetConcatInvNo_New', 'sp_WMSFull_CreatePackStr')
			insert #SQL select 'SET QUOTED_IDENTIFIER ON' + char(13) + char(10)
		else
			insert #SQL select 'SET QUOTED_IDENTIFIER OFF' + char(13) + char(10)
			
		insert #SQL select 'Go' + char(13) + char(10)
		insert #SQL select 'SET ANSI_NULLS ON ' + char(13) + char(10)
		insert #SQL select 'Go' + char(13) + char(10)
		
		insert #SQL 
		exec('sp_helptext '''+ @Name +'''')
		
		insert #SQL select char(13) + char(10) + 'Go' + char(13) + char(10)
		insert #SQL select 'SET QUOTED_IDENTIFIER OFF' + char(13) + char(10)
		insert #SQL select 'Go' + char(13) + char(10)
		insert #SQL select 'SET ANSI_NULLS ON ' + char(13) + char(10)
		insert #SQL select 'Go' + char(13) + char(10)

		--slow build, does alter script required for published objects in replication (these do not allow drops)
		if @ScriptBuildType = 'A'
		begin
			--now do the alter
			print @name
						
			update #SQL 
			set sql = replace(dbo.DBA_ConditionText(sql, @xtype), 'create ' + @Subject, 'alter ' + @Subject)
			where SQL like '%create%' + @Subject + '%'
		end
		
		--inserts the alter/drop/create script
		insert __Scripts (SQL, xtype)
		select sql, @xtype from #SQL    
		order by id
	 end

fetch next from PrincCursor into @name, @xtype
end 

close PrincCursor
deallocate PrincCursor

update __ScriptProgress set ScriptObject = 'Scripting Complete', oType = 'N/A', xCount = @tCount

select cast(SQL as varchar(8000)) from __Scripts order by id



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_GetNextCTLI_Int]') and xtype = 'P ')  
 drop Procedure sp_GetNextCTLI_Int
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_GetNextCTLI_Int TEST
CREATE proc [dbo].[sp_GetNextCTLI_Int]
    @CTLI_Key varchar(30)
as

SET NOCOUNT ON;

BEGIN TRAN
UPDATE RWS_CtlInt SET CTLI_Int=CTLI_Int+1 WHERE CTLI_Key=@CTLI_Key
IF @@rowcount=0
BEGIN
  INSERT INTO RWS_CtlInt (CTLI_Key, CTLI_Int)Values (@CTLI_Key, 1)
END
SELECT CTLI_Int FROM RWS_CtlInt WHERE CTLI_Key=@CTLI_Key
COMMIT TRAN


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_HM_DeleteAllAppUsers]') and xtype = 'P ')  
 drop Procedure sp_HM_DeleteAllAppUsers
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- sp_HM_DeleteAllAppUsers 'ReflexWS'
-- sp_HM_DeleteAllAppUsers 'HMCPortal' 

CREATE PROCEDURE [dbo].[sp_HM_DeleteAllAppUsers]
    @ApplicationName      nvarchar(256)
AS
BEGIN
    DECLARE @AppId uniqueidentifier

	SELECT @AppID=ApplicationId
	FROM aspnet_Applications
	WHERE LoweredApplicationName=LOWER(@ApplicationName)
    OR ApplicationName = @ApplicationName

    IF (@@ROWCOUNT = 0) -- Application Rec not found
    BEGIN
      SELECT 'Application Not Found'
      RETURN -1
    END

    DELETE aspnet_Membership
	WHERE ApplicationId=@AppID

    DELETE aspnet_Users
	WHERE ApplicationId=@AppID

    DELETE aspnet_Applications
	WHERE ApplicationId=@AppID
	
	DELETE RWS_Users
	DELETE RWS_LogActivity
	DELETE RWS_AppStats
 
    SELECT 'Successful'
END





Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddPrimaryKey]') and xtype = 'P ')  
 drop Procedure spAddPrimaryKey
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
--IF EXISTS (Select 1 from sysobjects where [name]='spAddPrimaryKey' and xtype='P')
--Drop procedure [dbo].[spAddPrimaryKey]
--GO

--spAddPrimaryKey 0, 1, 0, 1, 0 
create procedure [dbo].[spAddPrimaryKey] @Print bit = 1, @Execute bit = 0, @Identitycheck bit = 0, @PrintStatement bit = 0, @IncludeTempTable bit = 0 as 


/*************************************************************************************************************************************

????Store Procedure Name :: spAddPrimaryKey
????Created By???????????? :: Vinay Kumar

????Purpose???????????????? ::????Add Primary key in those tables which doesn't contain primary key.
???????????????????????? If table already contains any identity column with unique data then it'll convert it into a primary key
????????????????????????????OtherWise It'll add a new identity column with a primary key constraint.
????????????????????????????Primary key constraint = PK_+<Column_name>+<Table_Name>

????Input Parameters :: 5????
???????? @Print???????????????? Type = Bit
???????????????????????? 1 for Print Statement 
???????????????????????????? 0 for not Print Statement.
???????????????????????????? [Default Value :: 1]

???????? @Execute???????????? Type = Bit 
???????????????????????????? 1 for Execute Statement
???????????????????????????? 0 for Not Execute Statement.
???????????????????????????? [Default Value :: 0]

???????? @Identitycheck Type = Bit 
???????????????????????????? check that Is any identity column contains duplicate value. 
???????????????????????????? [Default Value :: 0]

???????? @PrintStatement???? Type = Bit 
???????????????????????????? This show resutl information that which tables are updated.
???????????????????????????? 1 for print statements.
???????????????????????????? [Default Value :: 0]

???????? @IncludeTempTable???? Type = Bit 
???????????????????????????? This Flag is used to include Temporary Tables.
???????????????????????????? 1 for include Temporary tables.
???????????????????????????? [Default Value :: 0] 


????Output Parameters :: 0

*************************************************************************************************************************************/

SET NOCOUNT ON 
-- Create Tmp Table 
create table #temp11 (id int identity(1,1), tid int ,tname varchar(200),CoLName varchar(200),isidentity bit,IsTempTable bit) 
 
insert into #temp11 (tid, tname, ColName, isidentity, IsTempTable) 
select id,[name],	bb.ColName,	case when bb.tabid is null then 0 else 1 end,
case when substring([name],0,4)='temp' then 1
	when substring([name],len([name])-3,4)='temp' then 1
	When substring([name],0,3)='temp' then 1
	When substring([name],len([name])-2,3)='temp' then 1
	else 0 end
from (select distinct id, [Name] 
		from sysobjects where xtype='U' 
		and id not in 
			(select distinct parent_obj from sysobjects where xtype in ('PK','UQ') 
			and parent_obj in (select id from sysobjects where xtype='U'))) aa 
left join (select c.object_id as [tabid],	c.[Name] as ColName,	s.[name] as tabName 
			from sys.objects s 
			inner join sys.columns c on s.object_id=c.object_id 
			where type='U' and c.is_identity=1) bb 
on aa.id=bb.tabid --where tabid is null


Declare @Counter int 
Declare @Maxid int 
Declare @Tname varchar(200) 
Declare @Tcol varchar(100) 
Declare @Tid int 
Declare @ColName varchar(200) 
Declare @isidentity bit 
Declare @SqlQuery nvarchar(4000) 
declare @Result varchar(8000) 
Declare @IdentitycheckString varchar(8000)
Declare @IsTempTable bit
 
 
set @Counter=1
set @IsTempTable=0 
set @Result='' 
set @IdentitycheckString='
---------- Show identity column information'
select @Maxid=max(id) from #temp11 
 
set @SqlQuery =' 
/***************************************************************************************************************************** 
 If any table which doesn''t contain Primary key, This script add the primary key. 
 Note:- If table already contains any identity column and contain unique data then it''ll converted in to primary key 
*****************************************************************************************************************************/  
' 
print(@SqlQuery) 

set @SQlQuery='
Select ''--------- Before Script ---------'' 
select distinct [Name] as ''Table which doesn''''t contain primary key'' from sysobjects where xtype=''U'' 
and id not in (select distinct parent_obj from sysobjects where xtype in (''PK'',''UQ'') 
and parent_obj in (select id from sysobjects where xtype=''U'')) order by 1'

Execute (@sqlQuery)


 
while (@counter<=@Maxid) 
begin 
	select @Tid=Tid, @Tname=Tname, @ColName=ColName, @isidentity=isidentity, @IsTempTable=IsTempTable 
	from #temp11 where id=@counter 
	
	select top 1 @Tcol= case when substring([Name],1,charindex('_',[Name]))='' then (Upper(substring(@Tname,1,3)))+'_' 
	else substring([Name],1,charindex('_',[Name])) end from syscolumns where id=object_id(@Tname) 

	 
	set @Tcol=@Tcol+'PKkey'

	if (@Identitycheck=1 and @isidentity=1)
	begin
		set @IdentitycheckString = @IdentitycheckString +'
		select '''+@Tname+''' as ''Table_Name'', '''+@ColName+''' as ''Identity_Column_Name'', 1 as ''ISDuplicateValue'' from '+@Tname+' group by '+@ColName+' having count(*)>1
		GO'
	end 

	if (@isidentity=1) 
	begin 
		set @SQlQuery=' 
		/********************************************************************* 
		 Table Name :: '+@Tname+', Column Name :: '+@ColName+' 
		*********************************************************************/ 
		IF NOT EXISTS (select top 1 * from '+@Tname+' group by '+@ColName+' having count(*)>1) 
		begin 
			IF NOT EXISTS(select top 1 * from sysobjects where sysobjects.[name]=''PK_'+@ColName+'_'+@Tname+''') 
			begin 
				alter table ['+@Tname+'] add constraint [PK_'+@ColName+'_'+@Tname+'] primary key (['+@ColName+']) 
			End 
		End' 
		set @Result=@Result+' 
		Print ''Table :: ['+@Tname +'] Primary Column :: ['+@ColName+']'' ' 
	end 

	else 
	begin  
		set @SQlQuery=' 
		/********************************************************************* 
		 Table Name :: '+@Tname+', Column Name :: '+@Tcol+' 
		*********************************************************************/ 
		IF NOT EXISTS(select top 1 * from sysobjects where sysobjects.[name]=''PK_'+@Tcol+'_'+@Tname+''') 
		begin 
			Alter table ['+@Tname+'] add ['+@Tcol+'] int identity(1,1) constraint [PK_'+@Tcol+'_'+@Tname+'] primary key 
		end' 
		set @Result=@Result+' 
		Print ''Table :: ['+@Tname +'] Primary Column :: ['+@Tcol+']'' ' 
	end 
	
	if (@Execute=1)
	begin 
		if (@IncludeTempTable=1)
			execute (@SQlQuery) 
		else if (@IsTempTable=0)
			execute (@SQlQuery) 
	end 
	
	if (@print=1) 
	begin 
		set @SQlQuery=@SQlQuery+' 
		GO ' 
		if (@IncludeTempTable=1)
			print (@SQlQuery)
		else
		begin
			if (@IsTempTable=0)
				print (@SQlQuery)
		end
end 

set @SQlQuery=''


if (@IdentityCheck=1) 
Print (@IdentityCheckString) 

if (@printstatement=1) 
print (@Result) 


set @SQlQuery=''
set @Result=' 
' 
set @IdentitycheckString='
'
set @counter=@counter+1 

	
end-- While end 
 
drop table #temp11 
set @SQlQuery='
Select ''--------- After Script ---------'' 
select distinct [Name] as ''Table which doesn''''t contain primary key'' from sysobjects where xtype=''U'' 
and id not in (select distinct parent_obj from sysobjects where xtype in (''PK'',''UQ'') 
and parent_obj in (select id from sysobjects where xtype=''U'')) order by 1'

Execute (@sqlQuery)

---- Execute statement

--EXECUTE spAddPrimaryKey 0,1,0,0,0






Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tools_CleanUpSequence]') and xtype = 'P ')  
 drop Procedure Tools_CleanUpSequence
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure Tools_CleanUpSequence
	@Table nvarchar(max),
	@idCol nvarchar(max),
	@SeqCol nvarchar(max),
    @WhereClause nvarchar(max)
as

--exec CleanUpSequence @Table = N'PPM_PublishExpectedAccomplishments',
--	@idCol = N'id',
--	@SeqCol = N'Seq',
--    @WhereClause = N'PPM_PublishPeriodID = 2 and ApplicablePeriod = 1'

--declare @Table nvarchar(max) = N'PPM_PublishExpectedAccomplishments',
--	@idCol nvarchar(max) = N'id',
--	@SeqCol nvarchar(max) = N'Seq',
--    @WhereClause nvarchar(max) = N'PPM_PublishPeriodID = 2 and ApplicablePeriod = 1'

declare @sql nvarchar(max)

set @sql = 
N'update ' + @table + N'
set ' + @SeqCol + N' = NewSeq
from (select ' + @idCol + N', ROW_NUMBER() over (order by ' + @SeqCol + N') [NewSeq] from ' + @Table + N'
where ' + @WhereClause + N') i
where i.' + @idCol + N' = ' + @Table + N'.' + @idCol +' and ' + @WhereClause

exec sp_executesql @sql



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tools_ReorderSeq]') and xtype = 'P ')  
 drop Procedure Tools_ReorderSeq
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

--/****** Object:  StoredProcedure [dbo].[Tools_ReorderSeq]    Script Date: 05/26/16 3:24:34 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER OFF
--GO

CREATE procedure [dbo].[Tools_ReorderSeq] 
	@id int, 
	@new_seq int,
	@Table nvarchar(max),
	@idCol nvarchar(max),
	@SeqCol nvarchar(max),
    @WhereClause nvarchar(max)
as 

--begin tran
begin

	--declare
	--	@id int = 9, 
	--	@new_seq int = 5,
	--	@Table nvarchar(max) = 'AttachmentStatus',
	--	@idCol nvarchar(max) = 'id',
	--	@SeqCol nvarchar(max) = 'Seq',
	--    @WhereClause nvarchar(max) = 'pri_id = 59'

	declare @sql nvarchar(max)

	set @sql =	
	
	'declare @old_seq int, @id int = ' + convert(nvarchar(20), @id) + ', @new_seq int = ' + convert(nvarchar(20), @new_seq) + '

	select @old_seq = ' + @SeqCol + '
	from ' + @Table + '
	where ' + @idCol + ' = @id

	if( @new_seq <> @old_seq )
	begin	
		select @old_seq=ISNULL(@old_seq, 9999)

		if(@new_seq > @old_seq)
		begin
			update ' + @Table + '
			set ' + @SeqCol + '=Seq-1
			where ' + @SeqCol + ' <= @new_seq and ' + @idCol + '<>@id  and ' + @WhereClause + '
		end

		update ' + @Table + '
		set ' + @SeqCol + '=' + @SeqCol + ' + 1
		where ' + @SeqCol + ' >= @new_seq and ' + @WhereClause + '

		update ' + @Table + ' 
		set ' + @SeqCol + ' = @new_seq
		where ' + @idCol + ' = @id

		exec Tools_CleanUpSequence @Table = ''' + @Table + ''', @idCol = ''' + @idCol + ''', @SeqCol = ''' + @SeqCol + ''', @WhereClause = ''' + replace(@WhereClause, '''', '''''') + '''

	end'

	print @sql

	exec sp_executesql @sql

end


--rollback tran


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_Applications]') and xtype = 'V ')  
 drop View vw_aspnet_Applications
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_MembershipUsers]') and xtype = 'V ')  
 drop View vw_aspnet_MembershipUsers
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_Profiles]') and xtype = 'V ')  
 drop View vw_aspnet_Profiles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_Roles]') and xtype = 'V ')  
 drop View vw_aspnet_Roles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_Users]') and xtype = 'V ')  
 drop View vw_aspnet_Users
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_UsersInRoles]') and xtype = 'V ')  
 drop View vw_aspnet_UsersInRoles
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_WebPartState_Paths]') and xtype = 'V ')  
 drop View vw_aspnet_WebPartState_Paths
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_WebPartState_Shared]') and xtype = 'V ')  
 drop View vw_aspnet_WebPartState_Shared
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_aspnet_WebPartState_User]') and xtype = 'V ')  
 drop View vw_aspnet_WebPartState_User
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

  CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]
 

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
