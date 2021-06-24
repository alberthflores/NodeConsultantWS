CREATE OR ALTER PROCEDURE spIncreaseNodeVues
AS
BEGIN
BEGIN TRANSACTION
	BEGIN TRY
		DECLARE @Id INT;
		DECLARE @RandomNumber INT;
		DECLARE @Now DATETIME=GETDATE();
		DECLARE db_cursor_nodes CURSOR FOR
			SELECT Id
			FROM [Node]
		OPEN db_cursor_nodes FETCH NEXT FROM db_cursor_nodes INTO @Id
			WHILE @@FETCH_STATUS = 0  
			BEGIN 
				/*Set random number beetwen 30-10*/
				SET @RandomNumber = (SELECT FLOOR(RAND()*(30-10+1))+10);
				/*Insert random number in to node value*/
				INSERT INTO NodeValue(NodeId,[Value],CreatedAt) values(@Id,@RandomNumber,@Now);

				FETCH NEXT FROM db_cursor_nodes INTO @Id
			END
		CLOSE db_cursor_nodes  
		DEALLOCATE db_cursor_nodes 
	END TRY
	BEGIN CATCH
		RAISERROR('ERROR EN LA BASE DE DATOS',16,1);
		ROLLBACK TRAN;
	END CATCH
	IF (@@TRANCOUNT <> 0)
	BEGIN
		COMMIT TRAN;
	END
END 
GO
