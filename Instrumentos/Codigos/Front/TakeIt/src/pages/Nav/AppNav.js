import React, { useContext } from 'react';
import { ActivityIndicator, StatusBar } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import Routes from '../../routes'
import { View } from 'react-native-animatable';
import { LoadingContext } from '../../context/LoadingContext';

export default function AppNav() {
  const {isLoading} = useContext(LoadingContext);

  if (isLoading) {
    return (
      <View style={{flex:1, justifyContent:'center',alignItems:'center'}}>
        <ActivityIndicator size={'large'} color={'#7ED957'} animating={true} />
      </View>
    );
  }
  
  return (
    <NavigationContainer>
      <StatusBar backgroundColor="#38A69D" barStyle="light-content" />
      <Routes/>
    </NavigationContainer>
  ); 
}
