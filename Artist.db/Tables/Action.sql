create table dbo.Action
(
  MasterId   bigint not null,
  CustomerId bigint not null,
  CustomerName nvarchar(300) not null,
  Created datetime2 not null,
  ExpirationDate datetime2 not null,
  CustomerPan varchar(50) not null,

  constraint pk_Artist primary key clustered (MasterId, CustomerId),

  constraint fk_Action_MasterId foreign key(MasterId) references Master(Id)
)