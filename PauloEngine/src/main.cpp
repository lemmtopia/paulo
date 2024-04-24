#include "game.h"

extern bool isRunning;

int main(int argc, char* argv[])
{
	InitializeWindow(640, 480, "Hello world!");
	Run();
	DestroyWindow();
	return 0;
}