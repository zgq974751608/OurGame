﻿
using System;
using System.Threading;
using System.Collections.Generic;
using Frankfort.Threading;
using Frankfort.Threading.Internal;
using UnityEngine;





/// <summary>
/// Primairy accespoint/interface to all framework methods & helpers. This is just a static wrapper-class for sake of ease...
/// </summary>
public static class UThread
{




    //--------------------------------------- 2 START SINGLE THREAD OVERLOADS --------------------------------------
    //--------------------------------------- 2 START SINGLE THREAD OVERLOADS --------------------------------------
    #region 2 START SINGLE THREAD OVERLOADS 

    
        
    /// <summary>
    /// Starts an method running on a new thread. The Thread dies when the method has stopped running.
    /// You can now make use of the DispatchToMainThread-actions & WaitForNextFrame
    /// </summary>
    /// <param name="targetMethod">The method that will be executed by the new thread</param>
    /// <param name="priority">Thread Priority</param>
    /// <param name="safeMode">Default TRUE: Executes the targetMethod within a Try-Catch statement and will log any errors back to the MainThread</param>
    /// <returns>Newly instantiated Thread</returns>
    public static Thread StartSingleThread(ThreadStart targetMethod, System.Threading.ThreadPriority priority = System.Threading.ThreadPriority.Normal, bool safeMode = true)
    {   
        return SingleThreadStarter.StartSingleThread(targetMethod, priority, safeMode);
    } 
    
    


    /// <summary>
    /// Starts an method running on a new thread. The Thread dies when the method has stopped running.
    /// You can now make use of the DispatchToMainThread-actions & WaitForNextFrame
    /// </summary>
    /// <param name="targetMethod">The method that will be executed by the new thread</param>
    /// <param name="argument">Object to pass to the targetMethod as soon as the Thread is started</param>
    /// <param name="priority">Thread Priority</param>
    /// <param name="safeMode">Default TRUE: Executes the targetMethod within a Try-Catch statement and will log any errors back to the MainThread</param>
    /// <returns>Newly instantiated Thread</returns>
    public static Thread StartSingleThread(ParameterizedThreadStart targetMethod, object argument, System.Threading.ThreadPriority priority = System.Threading.ThreadPriority.Normal, bool safeMode = true)
    {
        return SingleThreadStarter.StartSingleThread(targetMethod, argument, priority, safeMode);
    }


    #endregion
    //--------------------------------------- 2 START SINGLE THREAD OVERLOADS --------------------------------------
    //--------------------------------------- 2 START SINGLE THREAD OVERLOADS --------------------------------------
			












    //--------------------------------------- 4 MULTI THREADED WORK EXECUTION OVERLOADS --------------------------------------
    //--------------------------------------- 4 MULTI THREADED WORK EXECUTION OVERLOADS --------------------------------------

    #region 4 MULTI THREADED WORK EXECUTION OVERLOADS
    /// <summary>
    /// Helps spreading the same repetetive workload over multiple threads/cores in an easy way.
    /// </summary>
    /// <typeparam name="T">T: Generic-Type of the object you want to be computed by the executor</typeparam>
    /// <param name="staticWorkloadExecutor"> A (static) method that computes one workLoad-object at a time</param>
    /// <param name="workLoad">An array with objects you want to get computed by the executor</param>
    /// <param name="onComplete">Fired when all re-packaged workLoad-objects are finished computing</param>
    /// <param name="onPackageExecuted">Fires foreach finished re-packaged set of workLoad-object</param>
    /// <param name="maxThreads"> Lets you choose how many threads will be run simultaneously by the threadpool. Default: -1 == number of cores minus one, to make sure the MainThread has at least one core to run on. (quadcore == 1 core Mainthread, 3 cores used by the ThreadPoolScheduler)</param>
    /// <param name="scheduler">If Null, a new ThreadPoolScheduler will be instantiated.</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>A ThreadPoolScheduler that handles all the repackaged workLoad-Objects</returns>
    public static ThreadPoolScheduler StartMultithreadedWorkloadExecution<T>(ThreadWorkloadExecutor<T> workloadExecutor, T[] workLoad, MultithreadedWorkloadComplete<T> onComplete, MultithreadedWorkloadPackageComplete<T> onPackageComplete, int maxThreads = -1, ThreadPoolScheduler scheduler = null, bool safeMode = true)
    {
        return MultithreadedWorkloadHelper.StartMultithreadedWorkloadExecution<ThreadWorkloadExecutor<T>, T>(workloadExecutor, workLoad, null, onComplete, onPackageComplete, maxThreads, scheduler, safeMode);
    }



    /// <summary>
    /// Helps spreading the same repetetive workload over multiple threads/cores in an easy way.
    /// Besides the workLoad-object, the current index of the workLoad-array is passed to the Executor-delegate.
    /// </summary>
    /// <typeparam name="T">T: Generic-Type of the object you want to be computed by the executor</typeparam>
    /// <param name="staticWorkloadExecutor"> A (static) method that computes one workLoad-object at a time</param>
    /// <param name="workLoad">An array with objects you want to get computed by the executor</param>
    /// <param name="onComplete">Fired when all re-packaged workLoad-objects are finished computing</param>
    /// <param name="onPackageExecuted">Fires foreach finished re-packaged set of workLoad-object</param>
    /// <param name="maxThreads"> Lets you choose how many threads will be run simultaneously by the threadpool. Default: -1 == number of cores minus one, to make sure the MainThread has at least one core to run on. (quadcore == 1 core Mainthread, 3 cores used by the ThreadPoolScheduler)</param>
    /// <param name="scheduler">If Null, a new ThreadPoolScheduler will be instantiated.</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>A ThreadPoolScheduler that handles all the repackaged workLoad-Objects</returns>
    public static ThreadPoolScheduler StartMultithreadedWorkloadExecution<T>(ThreadWorkloadExecutorIndexed<T> workloadExecutor, T[] workLoad, MultithreadedWorkloadComplete<T> onComplete, MultithreadedWorkloadPackageComplete<T> onPackageComplete, int maxThreads = -1, ThreadPoolScheduler scheduler = null, bool safeMode = true)
    {
        return MultithreadedWorkloadHelper.StartMultithreadedWorkloadExecution<ThreadWorkloadExecutorIndexed<T>, T>(workloadExecutor, workLoad, null, onComplete, onPackageComplete, maxThreads, scheduler, safeMode);
    }


    /// <summary>
    /// Helps spreading the same repetetive workload over multiple threads/cores in an easy way.
    /// Besides the workLoad-object, an extra Argument will be passed to the Executor-delegate
    /// </summary>
    /// <typeparam name="T">T: Generic-Type of the object you want to be computed by the executor</typeparam>
    /// <param name="staticWorkloadExecutor"> A (static) method that computes one workLoad-object at a time</param>
    /// <param name="workLoad">An array with objects you want to get computed by the executor</param>
    /// <param name="onComplete">Fired when all re-packaged workLoad-objects are finished computing</param>
    /// <param name="onPackageExecuted">Fires foreach finished re-packaged set of workLoad-object</param>
    /// <param name="maxThreads"> Lets you choose how many threads will be run simultaneously by the threadpool. Default: -1 == number of cores minus one, to make sure the MainThread has at least one core to run on. (quadcore == 1 core Mainthread, 3 cores used by the ThreadPoolScheduler)</param>
    /// <param name="scheduler">If Null, a new ThreadPoolScheduler will be instantiated.</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>A ThreadPoolScheduler that handles all the repackaged workLoad-Objects</returns>
    public static ThreadPoolScheduler StartMultithreadedWorkloadExecution<T>(ThreadWorkloadExecutorArg<T> workloadExecutor, T[] workLoad, object extraArgument, MultithreadedWorkloadComplete<T> onComplete, MultithreadedWorkloadPackageComplete<T> onPackageComplete, int maxThreads = -1, ThreadPoolScheduler scheduler = null, bool safeMode = true)
    {
        return MultithreadedWorkloadHelper.StartMultithreadedWorkloadExecution<ThreadWorkloadExecutorArg<T>, T>(workloadExecutor, workLoad, extraArgument, onComplete, onPackageComplete, maxThreads, scheduler, safeMode);
    }


    /// <summary>
    /// Helps spreading the same repetetive workload over multiple threads/cores in an easy way.
    /// Besides the workLoad-object, an extra Argument & the current index of the workLoad-array is passed to the Executor-delegate.
    /// </summary>
    /// <typeparam name="T">T: Generic-Type of the object you want to be computed by the executor</typeparam>
    /// <param name="staticWorkloadExecutor"> A (static) method that computes one workLoad-object at a time</param>
    /// <param name="workLoad">An array with objects you want to get computed by the executor</param>
    /// <param name="onComplete">Fired when all re-packaged workLoad-objects are finished computing</param>
    /// <param name="onPackageExecuted">Fires foreach finished re-packaged set of workLoad-object</param>
    /// <param name="maxThreads"> Lets you choose how many threads will be run simultaneously by the threadpool. Default: -1 == number of cores minus one, to make sure the MainThread has at least one core to run on. (quadcore == 1 core Mainthread, 3 cores used by the ThreadPoolScheduler)</param>
    /// <param name="scheduler">If Null, a new ThreadPoolScheduler will be instantiated.</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>A ThreadPoolScheduler that handles all the repackaged workLoad-Objects</returns>
    public static ThreadPoolScheduler StartMultithreadedWorkloadExecution<T>(ThreadWorkloadExecutorArgIndexed<T> workloadExecutor, T[] workLoad, object extraArgument, MultithreadedWorkloadComplete<T> onComplete, MultithreadedWorkloadPackageComplete<T> onPackageComplete, int maxThreads = -1, ThreadPoolScheduler scheduler = null, bool safeMode = true)
    {
        return MultithreadedWorkloadHelper.StartMultithreadedWorkloadExecution<ThreadWorkloadExecutorArgIndexed<T>, T>(workloadExecutor, workLoad, extraArgument, onComplete, onPackageComplete, maxThreads, scheduler, safeMode);
    }

    #endregion

    //--------------------------------------- 4 MULTI THREADED WORK EXECUTION OVERLOADS --------------------------------------
    //--------------------------------------- 4 MULTI THREADED WORK EXECUTION OVERLOADS --------------------------------------
			







    






    //--------------------------------------- THREAD POOL SCHEDULAR --------------------------------------
    //--------------------------------------- THREAD POOL SCHEDULAR --------------------------------------

    #region THREAD POOL SCHEDULAR


    /// <summary>
    /// Unlike "StartMultithreadedWorkloadExecution", you will have to build your own IThreadWorkerObject.
    /// Downside: It requires some extra work. Upside: you got more controll over what goes in and comes out
    /// Infact: You can create you own polymorphed IThreadWorkerObject-array, each ellement being a completely different type. For example: the statemachines of enemies are IThreadWorkerObject's and the array contains completely different classes with enemies/AI-behaviours.
    /// </summary>
    /// <param name="workerObjects">An array of IThreadWorkerObject objects to be handled by the threads. If you want multiple cores/threads to be active, make sure that the number of IThreadWorkerObject's proves matches/exeeds your preferred number maxWorkingThreads. </param>
    /// <param name="onComplete">Fired when all re-packaged workLoad-objects are finished computing</param>
    /// <param name="onPackageExecuted">Fires foreach finished re-packaged set of workLoad-object</param>
    /// <param name="maxThreads"> Lets you choose how many threads will be run simultaneously by the threadpool. Default: -1 == number of cores minus one, to make sure the MainThread has at least one core to run on. (quadcore == 1 core Mainthread, 3 cores used by the ThreadPoolScheduler)</param>
    /// <param name="scheduler">If Null, a new ThreadPoolScheduler will be instantiated.</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>A ThreadPoolScheduler that handles all the repackaged workLoad-Objects</returns>
    public static ThreadPoolScheduler StartMultithreadedWorkerObjects(IThreadWorkerObject[] workerObjects, ThreadPoolSchedulerEvent onCompleteCallBack, ThreadedWorkCompleteEvent onPackageExecuted = null, int maxThreads = -1, ThreadPoolScheduler scheduler = null, bool safeMode = true)
    {
        if (scheduler == null)
            scheduler = CreateThreadPoolScheduler();

        scheduler.StartASyncThreads(workerObjects, onCompleteCallBack, onPackageExecuted, maxThreads, safeMode);
        return scheduler;
    }



    /// <summary>
    /// Creates an ThreadPoolScheduler instance, which happens to be a Monobehaviour and therefore will showup as gameobject "ThreadPoolScheduler";
    /// </summary>
    /// <returns></returns>
    public static ThreadPoolScheduler CreateThreadPoolScheduler()
    {
        GameObject go = new GameObject("ThreadPoolScheduler");
        return go.AddComponent<ThreadPoolScheduler>();
    }


    /// <summary>
    /// Creates an ThreadPoolScheduler instance, which happens to be a Monobehaviour and therefore will showup by the name you provide (default: "ThreadPoolScheduler");
    /// </summary>
    /// <returns></returns>
    public static ThreadPoolScheduler CreateThreadPoolScheduler(string name)
    {
        GameObject go = new GameObject(name == null || name == string.Empty ? "ThreadPoolScheduler" : name);
        return go.AddComponent<ThreadPoolScheduler>();
    }


    #endregion

    //--------------------------------------- THREAD POOL SCHEDULAR --------------------------------------
    //--------------------------------------- THREAD POOL SCHEDULAR --------------------------------------















    //--------------------------------------- THREAD WAIT COMMANDS --------------------------------------
    //--------------------------------------- THREAD WAIT COMMANDS --------------------------------------

    #region THREAD WAIT COMMANDS

    /// <summary>
    /// Halts/Sleeps the current thread until Unity has rendered a frame. This can be used Coroutine-wise and is safe to be placed within an endless while-loop. Not recommended though...
    /// If fired from the MainThread, an error-log will be thrown because its not allowed to freeze the MainThread.
    /// </summary>
    /// <param name="waitFrames">By default set to 1. You can let the Thread wait several frames if needed</param>
    public static void WaitForNextFrame(int waitFrames = 1)
    {
        new ThreadWaitForNextFrame(waitFrames);
    }


    /// <summary>
    /// Halts/Sleeps the current thread until "seconds" has expired. This can be used Coroutine-wise and is safe to be placed within an endless while-loop. Not recommended though...
    /// If fired from the MainThread, an error-log will be thrown because its not allowed to freeze the MainThread.
    /// </summary>
    /// <param name="seconds">Amount of time you want the Thread to sleep.</param>
    public static void WaitForSeconds(float seconds)
    {
        new ThreadWaitForSeconds(seconds);
    }

    #endregion

    //--------------------------------------- THREAD WAIT COMMANDS --------------------------------------
    //--------------------------------------- THREAD WAIT COMMANDS --------------------------------------
			













    //--------------------------------------- 4 DISPATCHER OVERLOADS --------------------------------------
    //--------------------------------------- 4 DISPATCHER OVERLOADS --------------------------------------

    #region 4 DISPATCHER OVERLOADS



    /// <summary>
    /// Fire and forget: The MainThread will execute this method witout any arguments to pass, nothing will be returned.
    /// </summary>
    /// <param name="dispatchCall">Example: "() => Debug.Log("This will be fired from the MainThread: " + System.Threading.Thread.CurrentThread.Name)" </param>
    /// <param name="waitForExecution">Freezes the thread, waiting for the MainThread to execute & finish the "dispatchCall".</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    public static void DispatchToMainThread(ThreadDispatchDelegate dispatchCall, bool waitForExecution = false, bool safeMode = true)
    {
        MainThreadDispatcher.DispatchToMainThread(dispatchCall, waitForExecution, safeMode);
    }


    
    
    /// <summary>
    /// When executed by the MainThread, this argument will be passed to the "dispatchCall";
    /// </summary>
    /// <param name="dispatchCall">Example: "(object obj) => Debug.Log("This will be fired from the MainThread: " + System.Threading.Thread.CurrentThread.Name + "\nObject: " + obj.toString())"</param>
    /// <param name="dispatchArgument">Once the MainThread executes this action, the argument will be passed to the delegate</param>
    /// <param name="waitForExecution">Freezes the thread, waiting for the MainThread to execute & finish the "dispatchCall".</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    public static void DispatchToMainThread(ThreadDispatchDelegateArg dispatchCall, object dispatchArgument, bool waitForExecution = false, bool safeMode = true)
    {
        MainThreadDispatcher.DispatchToMainThread(dispatchCall, dispatchArgument, waitForExecution, safeMode);
    }




    /// <summary>     
    /// When executed by the MainThread, this argument will be passed to the "dispatchCall";
    /// Allows you to dispatch an delegate returning an object (for example: a newly instantiated gameobject) that is directly available in your ThreadPool-Thread.
    /// Because the thread you are dispatching from is not the MainThread, your ThreadPool-thread needs to wait till the MainThread executed the method, and the returned value can be used directly
    /// </summary>
    /// <param name="dispatchCall">Example: "(object obj) => Debug.Log("This will be fired from the MainThread: " + System.Threading.Thread.CurrentThread.Name + "\nObject: " + obj.toString())"</param>
    /// <param name="dispatchArgument">Once the MainThread executes this action, the argument will be passed to the delegate</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>After the MainThread has executed the "dispatchCall" (and the worker-thread has been waiting), it will return whatever the dispatchCall returns to the worker-thread</returns>
    public static object DispatchToMainThreadReturn(ThreadDispatchDelegateArgReturn dispatchCall, object dispatchArgument, bool safeMode = true)
    {
        return MainThreadDispatcher.DispatchToMainThreadReturn(dispatchCall, dispatchArgument, safeMode);
    }


    /// <summary>
    /// Allows you to dispatch an delegate returning an object (for example: a newly instantiated gameobject) that is directly available in your ThreadPool-Thread.
    /// Because the thread you are dispatching from is not the MainThread, your ThreadPool-thread needs to wait till the MainThread executed the method, and the returned value can be used directly
    /// </summary>
    /// <param name="dispatchCall">Example: "(object obj) => Debug.Log("This will be fired from the MainThread: " + System.Threading.Thread.CurrentThread.Name + "\nObject: " + obj.toString())"</param>
    /// <param name="safeMode">Executes all the computations within try-catch events, logging it the message + stacktrace</param>
    /// <returns>After the MainThread has executed the "dispatchCall" (and the worker-thread has been waiting), it will return whatever the dispatchCall returns to the worker-thread</returns>
    public static object DispatchToMainThreadReturn(ThreadDispatchDelegateReturn dispatchCall, bool safeMode = true)
    {
        return MainThreadDispatcher.DispatchToMainThreadReturn(dispatchCall, safeMode);
    }
    

    #endregion

    //--------------------------------------- 4 DISPATCHER OVERLOADS --------------------------------------
    //--------------------------------------- 4 DISPATCHER OVERLOADS --------------------------------------










    //--------------------------------------- MAIN THREAD WATCHDOG --------------------------------------
    //--------------------------------------- MAIN THREAD WATCHDOG --------------------------------------
    #region MAIN THREAD WATCHDOG

    /// <summary>
    /// If you need your current code to be running on the MainThread, you can always call this method to check if its the MainThread or not...
    /// </summary>
    /// <returns>Returns TRUE if its the MainThread</returns>
    public static bool CheckIfMainThread()
    {
        return MainThreadWatchdog.CheckIfMainThread();
    }

    #endregion
    //--------------------------------------- MAIN THREAD WATCHDOG --------------------------------------
    //--------------------------------------- MAIN THREAD WATCHDOG --------------------------------------
			
}