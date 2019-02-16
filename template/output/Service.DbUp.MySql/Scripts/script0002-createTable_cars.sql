CREATE TABLE IF NOT EXISTS `cars` (
  `id` varchar(36) NOT NULL,
  `modelname` varchar(45) NOT NULL, 
  `cartype` tinyint(1) NOT NULL,
  `createdon` datetime NOT NULL,
  `modifiedon` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
