% original
[y, Fs] = audioread('1.wav');
disp('Press To Play Original Audio');
pause
sound(y, Fs);

% delay
blank = zeros(2 * Fs, 2);
y1 = [blank;y];
disp('Press To Play Audio After Delayed For 2s');
pause
sound(y1, Fs);

% flip
y2 = flipud(y);
disp('Press To Play Reversed Audio');
pause
sound(y2, Fs);

% faster
disp('Press To Play Faster Audio');
pause
sound(y, Fs * 1.5);

% slower
disp('Press To Play Slower Audio');
pause
sound(y, Fs * 0.75);

% low pass
 bl = fir1(100, 500/(Fs/2), 'low'); 
 y5 = filter(bl,1,y);
 disp('Press To Play Audio Without Frequency Above 500Hz');
 pause
 sound(y5, Fs);
 
 % high pass
 bh = fir1(100, 500/(Fs/2), 'high');
 y6 = filter(bh, 1, y);
 pause
 disp('Press To Play Audio Without Frequency Below 500Hz');
 sound(y6, Fs);
 
 
 audiowrite('delay.wav', y1, Fs);
 audiowrite('flip.wav', y2, Fs);
 audiowrite('faster.wav', y, 1.5 * Fs);
 audiowrite('slower.wav', y, 0.75 * Fs);
 audiowrite('lowpass.wav', y5, Fs);
 audiowrite('highpass.wav', y6, Fs);
