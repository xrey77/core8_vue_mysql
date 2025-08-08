function list_child_processes () {
    local ppid=$1;
    local current_children=$(pgrep -P $ppid);
    local local_child;
    if [ $? -eq 0 ];
    then
        for current_child in $current_children
        do
          local_child=$current_child;
          list_child_processes $local_child;
          echo $local_child;
        done;
    else
      return 0;
    fi;
}

ps 85595;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 85595 > /dev/null;
done;

for child in $(list_child_processes 85788);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/reynald/My-Programs/DotnetCore/core8_vue_mysql/bin/Debug/net8.0/408cd023f26d466c8a5f355e3d568eed.sh;
