USE bbomDb2
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('UpdateRoleForNotPayUsers', 'P') IS NOT NULL
  DROP PROCEDURE UpdateRoleForNotPayUsers;
GO
CREATE PROCEDURE UpdateRoleForNotPayUsers
AS
  BEGIN
    SET NOCOUNT ON;
    DECLARE @id NVARCHAR(128)
    DECLARE users CURSOR LOCAL SCROLL FOR
      SELECT u.id
      FROM bbomDb2.dbo.AspNetUsers u
        JOIN bbomDb2.dbo.Payments p
          ON p.id IN (SELECT TOP 1 id
                      FROM bbomDb2.dbo.Payments
                      WHERE UserId = u.Id AND Status = 1
                      ORDER BY Date DESC)
      WHERE p.EndDate <= GETDATE() AND u.Id NOT IN (SELECT u.Id
                                                    FROM bbomDb2.dbo.AspNetUsers u
                                                      JOIN bbomDb2.dbo.AspNetUserRoles ur
                                                        ON u.Id = ur.UserId
                                                      JOIN bbomDb2.dbo.AspNetRoles r
                                                        ON ur.RoleId = r.Id
                                                    WHERE r.Name = 'notPay')
    OPEN users
    FETCH NEXT FROM users
    INTO @id
    WHILE @@fetch_status = 0
      BEGIN
        INSERT INTO bbomDb2.dbo.AspNetUserRoles (UserId, RoleId) VALUES (@id, '02f990cc-cbf5-4db2-8d5d-a8ad98782b0b')
        FETCH NEXT FROM users
        INTO @id
      END
  END
GO
