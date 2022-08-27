CREATE PROCEDURE [dbo].[my_InsertUpdateDelete_Program]  
@ProgramID INT = NULL  
,@ProgramName NVARCHAR(100) = NULL  
,@DepartmentID INT = NULL  
,@Query INT  
AS  
BEGIN  
IF (@Query = 1)  
BEGIN  

INSERT INTO Program(  
Dep_Id
,Name
)  
VALUES (  
@DepartmentID  
,@ProgramName  
)  
IF (@@ROWCOUNT > 0)  
BEGIN  
SELECT 'Insert'  
END  
END  
IF (@Query = 2)  
BEGIN  
UPDATE Program  
SET Name = @ProgramName  
,Dep_Id = @DepartmentID  
WHERE Program.PId = @ProgramID  
SELECT 'Update'  
END  
IF (@Query = 3)  
BEGIN  
DELETE  
FROM Program  
WHERE Program.PId = @ProgramID  
SELECT 'Deleted'  
END  
IF (@Query = 4)  
BEGIN  
SELECT *  
FROM Program  
END  
END  
IF (@Query = 5)  
BEGIN  
SELECT *  
FROM Program  
WHERE  Program.PId = @ProgramID  
END  