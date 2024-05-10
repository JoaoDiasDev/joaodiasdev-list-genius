for i in `find /home/database -name "*.sql" | sort --version-sort`; do mysql -udocker -pjoaodiasdev!#$%@AMD1604!!@ joaodiasdev-product-list < $i; done;
