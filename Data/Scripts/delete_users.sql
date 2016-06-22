delete from identityDb.dbo.AspNetUsers
  WHERE Id IN (SELECT DISTINCT u.Id from identityDb.dbo.AspNetUsers u
  JOIN identityDb.dbo.AspNetUserRoles ur
  ON u.Id = ur.UserId
  JOIN identityDb.dbo.AspNetRoles r
  ON ur.RoleId = r.Id
WHERE u.Email is null
AND u.Foot is NULL
AND u.InvitedUser = u.parent_id
OR u.Email = 'test@doctor-c.ru'
and r.Name = 'notRegister')

DELETE from coinContentDb.dbo.UsersTemplateSettings
WHERE userid in (SELECT DISTINCT u.Id from identityDb.dbo.AspNetUsers u
  JOIN identityDb.dbo.AspNetUserRoles ur
  ON u.Id = ur.UserId
  JOIN identityDb.dbo.AspNetRoles r
  ON ur.RoleId = r.Id
WHERE u.Email is null
AND u.Foot is NULL
AND u.InvitedUser = u.parent_id
OR u.Email = 'test@doctor-c.ru'
and r.Name = 'notRegister')

DELETE from coinContentDb.dbo.ReceivedExtraRegParams
WHERE userid in (SELECT DISTINCT u.Id from identityDb.dbo.AspNetUsers u
  JOIN identityDb.dbo.AspNetUserRoles ur
  ON u.Id = ur.UserId
  JOIN identityDb.dbo.AspNetRoles r
  ON ur.RoleId = r.Id
WHERE u.Email is null
AND u.Foot is NULL
AND u.InvitedUser = u.parent_id
OR u.Email = 'test@doctor-c.ru'
and r.Name = 'notRegister')