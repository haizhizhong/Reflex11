
drop SYMMETRIC KEY SysCtrlTableKey
go
drop SYMMETRIC KEY ContactSyncTableKey
go
drop CERTIFICATE ReflexEncryptCert
go					  
drop MASTER KEY 
go
CREATE MASTER KEY ENCRYPTION
BY PASSWORD = 'ReflexAuthority1'
GO
open MASTER KEY DECRYPTION BY PASSWORD = 'ReflexAuthority1' 
CREATE CERTIFICATE ReflexEncryptCert
WITH SUBJECT = 'ReflexAuthority1'
go
CREATE SYMMETRIC KEY SysCtrlTableKey
WITH ALGORITHM = TRIPLE_DES ENCRYPTION
BY CERTIFICATE ReflexEncryptCert
go                                                            
CREATE SYMMETRIC KEY ContactSyncTableKey
WITH ALGORITHM = TRIPLE_DES ENCRYPTION
BY CERTIFICATE ReflexEncryptCert



