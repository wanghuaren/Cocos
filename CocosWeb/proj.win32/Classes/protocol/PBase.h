﻿#pragma once
#include "cocos2d.h"
using namespace cocos2d;
using namespace std;
class PBase
{
public:
	PBase();
public:
	virtual string getTabName(){
		return "";
	}
	virtual string getID(){
		return "";
	}
};

