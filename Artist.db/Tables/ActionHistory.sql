CREATE TABLE dbo.ActionHistory
(
  RecordId   bigint constraint pk_ActionHistory primary key clustered identity(1,1),
  MasterId   bigint not null,
  CustomerId bigint not null,
  CustomerName nvarchar(300) not null,
  Created datetime2 not null,
  ExpirationDate datetime2 not null,
  Deleted datetime2 null,
  CustomerPan varchar(50) not null,

  constraint fk_ActionHistory_MasterId foreign key(MasterId) references Master(Id)
)
