CREATE PROCEDURE dbo.clear_users_childs(@userName NVARCHAR(256)) AS
BEGIN
  SET NOCOUNT ON;
  DECLARE @id NVARCHAR(128)
  DECLARE @level INT
  DECLARE @un NVARCHAR(256)
  DECLARE users CURSOR LOCAL FOR
    WITH tree (parentId, userId, userName, Level)
    AS
    (
      -- Anchor member definition
      SELECT
        u.parent_id,
        u.Id,
        u.UserName,
        0 AS Level
      FROM bbomDb2.dbo.AspNetUsers AS u
      WHERE u.UserName = @userName
      UNION ALL
      -- Recursive member definition
      SELECT
        u.parent_id,
        u.Id,
        u.UserName,
        Level + 1
      FROM bbomDb2.dbo.AspNetUsers AS u
        INNER JOIN tree AS d
          ON u.parent_id = d.userId
    )
    SELECT userId
    FROM tree
    WHERE userName <> @userName
    ORDER BY Level DESC
  OPEN users
  FETCH NEXT FROM users
  INTO @id
  WHILE @@fetch_status = 0
    BEGIN
      DELETE FROM bbomDb2.dbo.AspNetUsers
      WHERE id = @id
      FETCH NEXT FROM users
      INTO @id
    END
END