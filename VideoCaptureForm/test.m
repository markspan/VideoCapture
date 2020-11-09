lsl = load_xdf( 'cams.xdf' );
%load cam1.mat
%load cam2.mat

close all

listFrames1 = str2double(lsl{1}.time_series);
listFrames2 = str2double(lsl{2}.time_series);
times1 = lsl{1}.time_stamps;
times2 = lsl{2}.time_stamps;
tt=1
while (tt>0)
    tt = tt + .1;
    %t = input("time:  ?");
    t = tt + times1(1);
    [t1, i1] = min(abs(times1 - t));
    [t2, i2] = min(abs(times2 - t));
      
    figure(1)
    image(cam1(listFrames1(i1)).cdata)
    figure(2)
    image(cam2(listFrames2(i2)).cdata)
end