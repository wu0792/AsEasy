/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2014                    */
/* Created on:     2016/6/12 22:08:15                           */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Answer')
            and   type = 'U')
   drop table Answer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LoginUser')
            and   type = 'U')
   drop table LoginUser
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OperLog')
            and   type = 'U')
   drop table OperLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Question')
            and   type = 'U')
   drop table Question
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Role')
            and   type = 'U')
   drop table Role
go

/*==============================================================*/
/* Table: Answer                                                */
/*==============================================================*/
create table Answer (
   AnswereID            bigint               identity,
   AnsweredBy           bigint               null,
   ContentType          tinyint              null,
   QuestionID           bigint               null,
   Content              binary(8000)         null,
   Status               tinyint              null,
   DataCreated_Date     datetime             null,
   DataCreated_By       bigint               null,
   DataChanged_Date     datetime             null,
   DataChanged_By       bigint               null,
   constraint PK_ANSWER primary key (AnswereID)
)
go

/*==============================================================*/
/* Table: LoginUser                                             */
/*==============================================================*/
create table LoginUser (
   UserID               bigint               identity,
   LoginName            varchar(20)          null,
   DispalyName          varchar(50)          null,
   Status               tinyint              null,
   Password             varchar(50)          null,
   DataCreated_Date     datetime             null,
   DataCreated_By       bigint               null,
   DataChanged_Date     datetime             null,
   DataChanged_By       bigint               null,
   constraint PK_LOGINUSER primary key (UserID)
)
go

/*==============================================================*/
/* Table: OperLog                                               */
/*==============================================================*/
create table OperLog (
   LogID                bigint               identity,
   DataType             int                  null,
   OperType             tinyint              null,
   EntityID             varchar(50)          null,
   Content              binary(8000)         null,
   DataCreated_Date     datetime             null,
   DataCreated_By       bigint               null,
   DataChanged_Date     datetime             null,
   DataChanged_By       bigint               null,
   constraint PK_OPERLOG primary key (LogID)
)
go

/*==============================================================*/
/* Table: Question                                              */
/*==============================================================*/
create table Question (
   QuestionID           bigint               identity,
   Title                varchar(100)         null,
   Remark               varchar(4000)        null,
   Status               tinyint              null,
   DataCreated_Date     datetime             null,
   DataCreated_By       bigint               null,
   DataChanged_Date     datetime             null,
   DataChanged_By       bigint               null,
   constraint PK_QUESTION primary key (QuestionID)
)
go

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role (
   RoleID               bigint               identity,
   Name                 varchar(100)         null,
   Remark               varchar(100)         null,
   DataCreated_Date     datetime             null,
   DataCreated_By       bigint               null,
   DataChanged_Date     datetime             null,
   DataChanged_By       bigint               null,
   constraint PK_ROLE primary key (RoleID)
)
go

